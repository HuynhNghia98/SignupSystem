using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Lecturer
{
	public class UpdateLecturerRequestDTO
	{
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
		public int ClassId { get; set; }
		public int? SecondClassId { get; set; }

		public IFormFile? File { get; set; }
	}
}
