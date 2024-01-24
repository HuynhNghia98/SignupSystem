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

namespace SignupSystem.Services.Auth
{
    public class AuthService : ControllerBase, IAuthService
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

		[HttpPost]
		public async Task<ApiResponse<object>> Login(LoginRequestDTO loginRequestDTO)
		{
			ApplicationUser user = _db.Users.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.Username.ToLower());

			if (user == null)
			{
				_res.IsSuccess = false;
				_res.StatusCode = HttpStatusCode.NotFound;
				ModelState.AddModelError(nameof(LoginRequestDTO.Username), "Email không tồn tại.");
				_res.Errors = ModelState.ToDictionary(
							 kvp => kvp.Key,
							 kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
						 );
				return _res;
			}

			var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

			if (isValid == false)
			{
				_res.IsSuccess = false;
				_res.StatusCode = HttpStatusCode.BadRequest;
				ModelState.AddModelError(nameof(LoginRequestDTO.Password), "Sai mật khẩu.");
				_res.Errors = ModelState.ToDictionary(
							 kvp => kvp.Key,
							 kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
						 );
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
				ModelState.AddModelError(nameof(LoginRequestDTO.Username), "Không thể đăng nhập.");
				_res.Errors = ModelState.ToDictionary(
							 kvp => kvp.Key,
							 kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
						 );
				_res.Result = new LoginRequestDTO();
				return _res;
			}

			_res.StatusCode = HttpStatusCode.OK;
			_res.Result = responseDTO;
			return _res;
		}

		private async Task<string> GenerateToken(ApplicationUser user)
		{
			//generate JWT Token
			var roles = await _userManager.GetRolesAsync(user);
			JwtSecurityTokenHandler tokenHandler = new();
			byte[] key = Encoding.ASCII.GetBytes(SecretKey);

			SecurityTokenDescriptor tokenDescriptor = new()
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("fullName",user.LastName+" "+user.FirstName),
					new Claim("id",user.Id.ToString()),
					new Claim(ClaimTypes.Email,user.UserName),
					new Claim(ClaimTypes.Role,roles.FirstOrDefault()),
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			};

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token).ToString();
		}
	}
}
