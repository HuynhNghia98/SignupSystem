using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.User
{
	public class AddOrUpdateUserRequestDTO
	{
		[Required(ErrorMessage ="Nhập tên người dùng")]
		public string UserName { get; set; } = string.Empty;
		public string UserEmail { get; set; } = string.Empty;
		[Required(ErrorMessage ="Chọn vai trò cho người dùng")]
		public string RoleName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập mật khẩu")]
		public string Password { get; set; } = string.Empty;
		public IFormFile? Image { get; set; }
	}
}
