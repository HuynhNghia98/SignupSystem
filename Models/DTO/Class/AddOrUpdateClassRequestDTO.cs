using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Class
{
	public class AddOrUpdateClassRequestDTO
	{
		[Required(ErrorMessage = "Chọn niên khóa")]
		public int TrainingCourseId { get; set; }
		[Required(ErrorMessage = "Chọn khoa - khối")]
		public int FacultyId { get; set; }
		[Required(ErrorMessage = "Nhập mã lớp")]
		public string ClassCode { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập tên lớp")]
		public string ClassName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập số lượng học viên của lớp")]
		public int StudentQuantity { get; set; }
		[Required(ErrorMessage = "Nhập học phí lớp")]
		public double Fee { get; set; }
		public string? Detail { get; set; } = string.Empty;
		public bool Status { get; set; } = false;
		public IFormFile? File { get; set; }

	}
}
