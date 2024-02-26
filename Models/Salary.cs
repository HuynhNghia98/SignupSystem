using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class Salary
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public double SalaryOfEmployee { get; set; }
		public string? AllowanceName { get; set; } = string.Empty;
		public double? Allowance { get; set; } = 0;
		public DateTime SalaryDay { get; set; } = DateTime.Now;
		public string? Notes { get; set; } = string.Empty;

		[Required]
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public int TrainingCourseId { get; set; }
		[ForeignKey("TrainingCourseId")]
		public TrainingCourse TrainingCourse { get; set; }
	}
}
