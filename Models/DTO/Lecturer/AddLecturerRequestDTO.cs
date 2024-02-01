using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Lecturer
{
	public class AddLecturerRequestDTO
	{
		[Required(ErrorMessage = "Vui lòng nhập Id.")]
		public string Id { get; set; }
		public string? TaxCode { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập tên.")]
		public string LastName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Vui lòng nhập họ.")]
		public string FirstName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Vui lòng nhập ngày sinh.")]
		public string DOB { get; set; } = string.Empty;
		[Required(ErrorMessage = "Vui lòng nhập giới tính.")]
		public int Gender { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập email.")]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public string? Address { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập Môn dạy chính.")]
		public int SubjectId { get; set; }
		public string? SecondSubject { get; set; } = string.Empty;

		[Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
		public string Password { get; set; } = string.Empty;

		public IFormFile? File { get; set; }
	}
}
