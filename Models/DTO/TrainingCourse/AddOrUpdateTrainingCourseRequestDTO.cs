using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.TrainingCourse
{
	public class AddOrUpdateTrainingCourseRequestDTO
	{
		[Required(ErrorMessage = "Nhập mã khóa")]
		public string TrainingCourseCode { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập tên khóa")]
		public string TrainingCourseName { get; set; } = string.Empty;

	}
}
