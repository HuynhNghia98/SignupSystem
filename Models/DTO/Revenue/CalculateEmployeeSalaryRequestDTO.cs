using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Revenue
{
	public class CalculateEmployeeSalaryRequestDTO
	{
		[Required(ErrorMessage = "Nhập mã khóa")]
		public int TrainingCourseId { get; set; }
		[Required(ErrorMessage = "Nhập mã giảng viên")]
		public string LecturerId { get; set; }
		[Required(ErrorMessage = "Nhập phần trăm lương")]
		public double PercentageOfSalaryPerStudent { get; set; }
	}
}
