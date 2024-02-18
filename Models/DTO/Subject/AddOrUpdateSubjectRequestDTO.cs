using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Class
{
	public class AddOrUpdateSubjectRequestDTO
	{
		[Required(ErrorMessage = "Nhập tên môn học")]
		public string ClassName { get; set; } = string.Empty;
		public string? Detail { get; set; } = string.Empty;
		[Required(ErrorMessage = "Chọn khoa")]
		public int FacultyId { get; set; }
		[Required(ErrorMessage = "Chọn bộ môn")]
		public int DepartmentId { get; set; }

	}
}
