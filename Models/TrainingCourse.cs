using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class TrainingCourse
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string TrainingCourseCode { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? Details { get; set; } = string.Empty;

		public ICollection<Class>? Classes { get; set; }
	}
}
