using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Student
{
	public class UpdateStudentRequestDTO
	{
		[Required(ErrorMessage = "Vui lòng nhập tên.")]
		public string LastName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Vui lòng nhập họ.")]
		public string FirstName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Vui lòng nhập ngày sinh.")]
		public string DOB { get; set; } = string.Empty;
		[Required(ErrorMessage = "Vui lòng nhập giới tính.")]
		public int Gender { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập email.")]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public string? PhoneNumer { get; set; } = string.Empty;
		public string? Address { get; set; } = string.Empty;
		public string? Parents { get; set; } = string.Empty;
		public IFormFile? File { get; set; }
	}
}
