using SignupSystem.Models.DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.Response;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SignupSystem.Services.Auth.Interfaces;
using SignupSystem.Models.DTO.ForgotPassword;
using SignupSystem.Utilities;

namespace SignupSystem.Services.Auth
{
    public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public ApiResponse<object> _res;
		private string SecretKey;

		public AuthService(IUnitOfWork unitOfWork,
			ApplicationDbContext db,
			RoleManager<IdentityRole> roleManager,
			UserManager<ApplicationUser> userManager,
			IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_db = db;
			_roleManager = roleManager;
			_userManager = userManager;
			_res = new();
			SecretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
		}

		public async Task<ApiResponse<object>> Login(LoginRequestDTO loginRequestDTO)
		{
			ApplicationUser user = _db.Users.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.Username.ToLower());

			if (user == null)
			{
				_res.IsSuccess = false;
				_res.StatusCode = HttpStatusCode.NotFound;
				_res.Errors = new Dictionary<string, List<string>>
						{
							{ nameof(LoginRequestDTO.Username), new List<string> { $"Email không tồn tại." }}
						};
				return _res;
			}

			var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

			if (isValid == false)
			{
				_res.IsSuccess = false;
				_res.StatusCode = HttpStatusCode.BadRequest;
				_res.Errors = new Dictionary<string, List<string>>
						{
							{ nameof(LoginRequestDTO.Password), new List<string> { $"Sai mật khẩu." }}
						};
				_res.Result = new LoginRequestDTO();
				return _res;
			}

			//generate JWT Token
			LoginResponseDTO responseDTO = new()
			{
				Email = user.Email,
				Token = await GenerateToken(user),
			};

			if (responseDTO.Email == null || string.IsNullOrEmpty(responseDTO.Token))
			{
				_res.IsSuccess = false;
				_res.StatusCode = HttpStatusCode.BadRequest;
				_res.Errors = new Dictionary<string, List<string>>
						{
							{ nameof(LoginRequestDTO.Username), new List<string> { $"Không thể đăng nhập." }}
						};
				_res.Result = new LoginRequestDTO();
				return _res;
			}

			_res.StatusCode = HttpStatusCode.OK;
			_res.Result = responseDTO;
			return _res;
		}

		private async Task<string> GenerateToken(ApplicationUser user)
		{
			//lấy danh sách role của người dùng
			var roles = await _userManager.GetRolesAsync(user);

			// lấy userClaims
			var userClaims = await _userManager.GetClaimsAsync(user);

			//lấy role của user
			var role = await _roleManager.FindByNameAsync(roles.FirstOrDefault());

			//lấy roleClaims
			var roleClaims = await _roleManager.GetClaimsAsync(role);

			JwtSecurityTokenHandler tokenHandler = new();
			byte[] key = Encoding.ASCII.GetBytes(SecretKey);

			List<Claim> claims = new List<Claim>
			{
				new Claim("fullName",user.LastName+" "+user.FirstName),
				new Claim("id",user.Id.ToString()),
				new Claim(ClaimTypes.Email,user.UserName),
				new Claim(ClaimTypes.Role,roles.FirstOrDefault())
			};

			//thêm các userClaims vào danh sách claims
			foreach (var userClaim in userClaims)
			{
				claims.Add(new Claim(userClaim.Type, userClaim.Value));
			}

			//thêm các roleClaims vào danh sách claims
			foreach (var roleClaim in roleClaims)
			{
				claims.Add(new Claim(roleClaim.Type, roleClaim.Value));
			}

			//generate JWT Token
			SecurityTokenDescriptor tokenDescriptor = new()
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			};

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token).ToString();
		}
	}
}
