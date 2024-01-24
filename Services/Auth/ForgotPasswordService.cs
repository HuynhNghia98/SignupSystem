using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Data;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.DTO.ForgotPassword;
using SignupSystem.Models.Response;
using SignupSystem.Services.Auth.Interfaces;
using SignupSystem.Utilities;

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
					ModelStateHelper.AddModelError<ForgotPasswordRequestDTO>(ModelState, nameof(ForgotPasswordRequestDTO.Email), "Email không tồn tại.");
					_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
					_res.IsSuccess = false;
					return _res;
				}

				var token = await _userManager.GeneratePasswordResetTokenAsync(user);

				string pStyle = "style='font-size:20px;'";
				string btnStyle = "style='color: white; background-color: #4CAF50; padding: 10px; border: none; border-radius: 5px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px;'";
				string mailBody = $"<p {pStyle}>Please reset your password by clicking here: <button {btnStyle}>Click</button></p>" +
					$"<p> <strong>UserId</strong>: {user.Id} </p>" +
					$"<p> <strong>Token</strong>: {token} </p>";

				// Gửi email chứa mã xác nhận
				await _mailService.SendEmailAsync(user.LastName, user.LastName, user.Email, "Reset Password",mailBody);

				_res.Messages = "Email đặt lại mật khẩu đã được gửi";
				return _res;
			}

			_res.IsSuccess = false;
			return _res;
		}

		[HttpPost]
		public async Task<ApiResponse<object>> ChangePassword(ChangePasswordRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == model.UserId);

				if (user == null)
				{
					ModelStateHelper.AddModelError<ChangePasswordRequestDTO>(ModelState, nameof(ChangePasswordRequestDTO.UserId), "Không tìm thấy người dùng");
					_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
					_res.IsSuccess = false;
					return _res;
				}

				var result = await _userManager.ResetPasswordAsync(user, model.ResetToken, model.Password);

				if (result.Succeeded)
				{
					_res.Messages = "Đổi mật khẩu thành công";
					return _res;
				}

				_res.IsSuccess = false;
				return _res;
			}

			_res.IsSuccess = false;
			return _res;
		}
	}
}
