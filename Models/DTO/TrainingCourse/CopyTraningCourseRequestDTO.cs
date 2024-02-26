using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.TrainingCourse
{
	public class CopyTraningCourseRequestDTO
	{
		[Required(ErrorMessage = "Nhập mã khóa")]
		public string TrainingCourseCode { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập tên khóa")]
		public string TrainingCourseName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Nhập ngày bắt đầu")]
		public DateTime StartDay { get; set; } 
		[Required(ErrorMessage = "Nhập ngày kết thúc")]
		public DateTime EndDay { get; set; }
	}
}
