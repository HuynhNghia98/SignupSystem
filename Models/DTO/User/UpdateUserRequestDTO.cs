using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.User
{
	public class UpdateUserRequestDTO
	{
		[Required(ErrorMessage ="Nhập tên người dùng")]
		public string UserName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập email người dùng")]
		[EmailAddress]
		public string UserEmail { get; set; } = string.Empty;
		[Required(ErrorMessage ="Chọn vai trò cho người dùng")]
		public string RoleName { get; set; } = string.Empty;
		public IFormFile? Image { get; set; }
	}
}
