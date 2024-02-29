using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.DTO.User;
using SignupSystem.Models.Response;
using SignupSystem.Services.AuthorizationManagement.Interface;
using SignupSystem.Utilities;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace SignupSystem.Services.AuthorizationManagement
{
	public class AuthorizationManagementService : IAuthorizationManagementService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IWebHostEnvironment _webHost;
		private ApiResponse<object> _res;
		public AuthorizationManagementService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IWebHostEnvironment webHost)
		{
			_unitOfWork = unitOfWork;
			_roleManager = roleManager;
			_userManager = userManager;
			_db = db;
			_res = new();
			_webHost = webHost;
		}
		public async Task<ApiResponse<GetUsersResponseDTO>> GetAndSearchUsersAsync(string? search)
		{
			ApiResponse<GetUsersResponseDTO> res = new();
			var usersInDb = new List<ApplicationUser>();
			if (string.IsNullOrEmpty(search))
			{
				usersInDb = await _unitOfWork.ApplicationUser.Get(x => x.IsEmployee == true, true).ToListAsync();
			}
			else
			{
				usersInDb = await _unitOfWork.ApplicationUser
					.Get(x => x.FirstName.Contains(search) && x.IsEmployee == true, true)
					.ToListAsync();
			}

			foreach (var item in usersInDb)
			{
				var role = _userManager.GetRolesAsync(item).GetAwaiter().GetResult().FirstOrDefault();
				if (role != null)
				{
					item.Role = role;
				}
			}

			res.Result.Users = usersInDb;
			return res;
		}

		public async Task<ApiResponse<ApplicationUser>> GetUserAsync(string userId)
		{
			ApiResponse<ApplicationUser> res = new();
			if (string.IsNullOrEmpty(userId))
			{
				res.IsSuccess = false;
				return res;
			}

			var userInDb = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);

			var roleOfUser = _userManager.GetRolesAsync(userInDb).GetAwaiter().GetResult().FirstOrDefault();

			if (roleOfUser != null)
			{
				userInDb.Role = roleOfUser;
			}

			res.Result = userInDb;
			return res;
		}

		public async Task<ApiResponse<object>> AddUserAsync(AddUserRequestDTO model)
		{
			var role = await _roleManager.FindByNameAsync(model.RoleName);
			if (role != null && model.Password != null)
			{
				var userInDb = await _unitOfWork.ApplicationUser
					.Get(x => x.FirstName.Equals(model.UserName) || x.UserName.Equals(model.UserName), true)
					.FirstOrDefaultAsync();

				//kiểm tra trùng username
				if (userInDb != null)
				{
					_res.Errors = new Dictionary<string, List<string>>
							{
								{ "error", new List<string> { $"Tên đã tồn tại" }}
							};
					_res.IsSuccess = false;
					return _res;
				}

				var newUser = new ApplicationUser()
				{
					FirstName = model.UserName,
					LastName = model.UserName,
					Email = model.UserEmail,
					UserCode = model.UserName,
					UserName = model.UserName,
					Gender = 0,
					PhoneNumber = "0",
					DOB = DateTime.Now,
					IsEmployee = true,
				};
				var result = await _userManager.CreateAsync(newUser, model.Password);

				if (!result.Succeeded)
				{
					_res.Errors = new Dictionary<string, List<string>>
							{
								{ "error", new List<string> { $"Không thể tạo mới" }}
							};
					_res.IsSuccess = false;
					return _res;
				}

				var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.FirstName == newUser.FirstName);
				if (user == null)
				{
					_res.Errors = new Dictionary<string, List<string>>
							{
								{ "error", new List<string> { $"Không tìm thấy người dùng đã tạo" }}
							};
					_res.IsSuccess = false;
					return _res;
				}

				//thêm claim cho người dùng
				UserClaim.AddClaimsToUser(user, _userManager, SD.ClaimList);

				//thêm role cho người dùng
				await _userManager.AddToRoleAsync(user, role.Name);

				//add image
				if (model.Image != null && model.Image.Length > 0)
				{
					//root path
					string wwwRootPath = _webHost.WebRootPath;
					string userImagePath = Path.Combine(wwwRootPath, @"images/user");
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
					using (var fileStream = new FileStream(Path.Combine(userImagePath, fileName), FileMode.Create))
					{
						model.Image.CopyTo(fileStream);
					}

					user.ImageUrl = @"\images\user" + fileName;
					_unitOfWork.ApplicationUser.Update(user);
					_unitOfWork.Save();
				}
				_res.Messages = "Tạo mới người dùng thành công";
				return _res;
			}
			else
			{
				_res.Errors = new Dictionary<string, List<string>>
						{
							{ "roleName", new List<string> { $"Vai trò không tồn tại." }}
						};
				_res.IsSuccess = false;
			}
			return _res;
		}

		public async Task<ApiResponse<object>> UpdateUserAsync(string userId, UpdateUserRequestDTO model)
		{
			if (string.IsNullOrEmpty(userId))
			{
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "userId", new List<string> { $"Không có id người dùng" }}
					};
				_res.IsSuccess = false;
				return _res;
			}

			var role = await _roleManager.FindByNameAsync(model.RoleName);

			if (role == null)
			{
				_res.Errors = new Dictionary<string, List<string>>
							{
								{ nameof(AddUserRequestDTO.RoleName), new List<string> { $"Vai trò không tồn tại." }}
							};
				_res.IsSuccess = false;
				return _res;
			}



			var userInDb = await _unitOfWork.ApplicationUser.Get(x => x.Id == userId, true).FirstOrDefaultAsync();
			if (userInDb == null)
			{
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "userId", new List<string> { $"Không tìm thấy người dùng" }}
					};
				_res.IsSuccess = false;
				return _res;
			}

			var userInDbToCheckUserName = await _unitOfWork.ApplicationUser
				.Get(x => (x.FirstName.Equals(model.UserName) || x.UserName.Equals(model.UserName)) && x.Id != userInDb.Id, true)
				.FirstOrDefaultAsync();

			//kiểm tra trùng tên
			if (userInDbToCheckUserName != null)
			{
				_res.Errors = new Dictionary<string, List<string>>
							{
								{ "error", new List<string> { $"Tên đã tồn tại" }}
							};
				_res.IsSuccess = false;
				return _res;
			}

			userInDb.FirstName = model.UserName; ;
			userInDb.Email = model.UserEmail;

			//update image
			if (model.Image != null && model.Image.Length > 0)
			{
				//root path
				string wwwRootPath = _webHost.WebRootPath;
				string userImagePath = Path.Combine(wwwRootPath, @"images/user");

				//remove image
				if (!string.IsNullOrEmpty(userInDb.ImageUrl))
				{
					string imagePath = Path.Combine(wwwRootPath, userInDb.ImageUrl.TrimStart('\\'));

					if (System.IO.File.Exists(imagePath))
					{
						System.IO.File.Delete(imagePath);
					}
				}

				//add new image
				string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
				using (var fileStream = new FileStream(Path.Combine(userImagePath, fileName), FileMode.Create))
				{
					model.Image.CopyTo(fileStream);
				}

				userInDb.ImageUrl = @"\images\user" + fileName;
			}
			_unitOfWork.ApplicationUser.Update(userInDb);
			_unitOfWork.Save();

			// Kiểm tra xem người dùng có role cũ không
			var oldRole = _userManager.GetRolesAsync(userInDb).GetAwaiter().GetResult().FirstOrDefault();
			if (oldRole != null)
			{
				// Xóa role cũ
				await _userManager.RemoveFromRoleAsync(userInDb, oldRole);
			}

			// Thêm role mới
			await _userManager.AddToRoleAsync(userInDb, role.Name);

			_res.Messages = "Cập nhật người dùng thành công";
			return _res;
		}

		public async Task<ApiResponse<object>> DeleteUserAsync(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Không có id người dùng" }}
					};
				_res.IsSuccess = false;
				return _res;
			}

			var userInDb = await _unitOfWork.ApplicationUser.Get(x => x.Id == userId, true).FirstOrDefaultAsync();

			if (userInDb == null)
			{
				_res.Errors = new Dictionary<string, List<string>>
				{
					{ "error", new List<string> { $"Không tìm thấy người dùng" }}
				};
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.ApplicationUser.Remove(userInDb);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa người dùng thành công";
			return _res;
		}

		//role claims

		public ApiResponse<GetRolesResponseDTO> GetRoles()
		{
			ApiResponse<GetRolesResponseDTO> res = new();
			// Lấy tất cả các role từ RoleManager
			var roles = _roleManager.Roles.ToList();

			// Chỉ lấy tên các role để trả về
			var roleNames = roles.Select(r => r.Name).ToList();

			res.Result.Roles = roleNames;
			return res;
		}

		public async Task<ApiResponse<GetClaimsResponseDTO>> GetRoleClaimsAsync(string roleName)
		{
			ApiResponse<GetClaimsResponseDTO> res = new();
			// Tìm kiếm role theo tên
			var role = await _roleManager.FindByNameAsync(roleName);

			if (role == null)
			{
				res.Errors = new Dictionary<string, List<string>>
				{
					{ "error", new List<string> { $"Không tìm thấy role" }}
				};
				res.IsSuccess = false;
				return res;
			}

			// Lấy danh sách các claims của role
			var roleClaims = await _roleManager.GetClaimsAsync(role);

			// Chuyển đổi danh sách claims thành định dạng phù hợp để trả về
			var formattedClaims = roleClaims.ToDictionary(
				c => c.Type,
				c => bool.Parse(c.Value)
			);

			res.Result.Claims = formattedClaims;
			return res;
		}

		public async Task<ApiResponse<object>> UpdateRoleClaimsAsync(UpdateRoleClaimRequestDTO model)
		{
			// Tìm kiếm role theo tên
			var role = await _roleManager.FindByNameAsync(model.RoleName);
			if (role == null)
			{
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Không tìm thấy role" }}
					};
				_res.IsSuccess = false;
				return _res;
			}

			// lấy danh sách role claim của role theo tên
			var roleClaims = await _roleManager.GetClaimsAsync(role);

			foreach (var newClaim in model.RoleClaims)
			{
				// kiểm trâ có tồn tại claim
				var existingClaim = roleClaims.FirstOrDefault(c => c.Type == newClaim.Key);

				if (existingClaim != null)
				{
					// nếu đã tồn tại, xóa claim cũ
					await _roleManager.RemoveClaimAsync(role, existingClaim);
					// add claim mới
					await _roleManager.AddClaimAsync(role, new Claim(newClaim.Key, newClaim.Value.ToString()));
				}
			}
			_res.Messages = "Cập nhật phân quyền thành công";
			return _res;
		}

		//user claims

		public async Task<ApiResponse<GetClaimsResponseDTO>> GetUserClaimsAsync(string userId)
		{
			ApiResponse<GetClaimsResponseDTO> res = new();

			if (string.IsNullOrEmpty(userId))
			{
				res.IsSuccess = false;
				return res;
			}

			//tìm kiếm user theo id
			var userInDb = await _userManager.FindByIdAsync(userId);

			if (userInDb == null)
			{
				res.Errors = new Dictionary<string, List<string>>
				{
					{ "error", new List<string> { $"Không tìm thấy người dùng" }}
				};
				res.IsSuccess = false;
				return res;
			}
			// Tìm kiếm claim theo user
			var userClaims = await _userManager.GetClaimsAsync(userInDb);

			// Chuyển đổi danh sách claims thành định dạng phù hợp để trả về
			var formattedClaims = userClaims.ToDictionary(
				c => c.Type,
				c => bool.Parse(c.Value)
			);

			res.Result.Claims = formattedClaims;
			return res;
		}

		public async Task<ApiResponse<object>> UpdateUserClaimsAsync(string userId, UpdateUserClaimsRequestDTO model)
		{
			if (string.IsNullOrEmpty(userId))
			{
				_res.IsSuccess = false;
				return _res;
			}

			//tìm kiếm user theo id
			var userInDb = await _userManager.FindByIdAsync(userId);

			if (userInDb == null)
			{
				_res.Errors = new Dictionary<string, List<string>>
				{
					{ "error", new List<string> { $"Không tìm thấy người dùng" }}
				};
				_res.IsSuccess = false;
				return _res;
			}

			UserClaim.UpdateClaimsToUser(userInDb, _userManager, model.newUserClaims);

			_res.Messages = "Cập nhật phân quyền người dùng thành công";
			return _res;
		}
	}
}
