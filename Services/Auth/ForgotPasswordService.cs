using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Data;
using SignupSystem.Models;
using SignupSystem.Models.DTO.ForgotPassword;
using SignupSystem.Models.Response;
using SignupSystem.Services.Auth.Interfaces;

namespace SignupSystem.Services.Auth
{
	public class ForgotPasswordService : ControllerBase, IForgotPasswordService
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly Interfaces.IMailService _mailService; // Đối tượng dịch vụ gửi email
		private ApiResponse<object> _res;

		public ForgotPasswordService(UserManager<ApplicationUser> userManager, Interfaces.IMailService mailService, ApplicationDbContext db)
		{
			_userManager = userManager;
			_mailService = mailService;
			_db = db;
			_res = new();
		}

		[HttpPost]
		public async Task<ApiResponse<object>> ForgotPassword(ForgotPasswordRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == model.Email);

				if (user == null)
				{
					_res.Messages = "Không tìm thấy email.";
					_res.IsSuccess = false;
					return _res;
				}

				var token = await _userManager.GeneratePasswordResetTokenAsync(user);

				// Tạo URL reset password với mã xác nhận
				//var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = token }, protocol: HttpContext.Request.Scheme);

				// Gửi email chứa mã xác nhận
				await _mailService.SendEmailAsync(user.LastName, user.LastName, user.Email, "Reset Password", $"Please reset your password by clicking here: <button>Click</button>");

				_res.Messages = "Email đặt lại mật khẩu đã được gửi";
				return _res;
			}

			_res.Messages = "Email không chính xác.";
			_res.IsSuccess = false;
			return _res;
		}
	}
}
