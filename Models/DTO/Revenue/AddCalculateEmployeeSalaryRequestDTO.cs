using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Revenue
{
	public class AddCalculateEmployeeSalaryRequestDTO
	{
		[Required(ErrorMessage = "Nhập lương thực nhận")]
		public double Salary { get; set; }
		[Required(ErrorMessage = "Nhập mã khóa đào tạo")]
		public int TrainingCourseId { get; set; }
		public string? AllowanceName { get; set; } = string.Empty;
		public double? Allowance { get; set; } = 0;
		public string? Notes { get; set; } = string.Empty;
	}
}
