using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class Class
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string ClassCode { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string SchoolYear { get; set; } = string.Empty;
		[Required]
		public string Status { get; set; } = string.Empty;
		[Required]
		public double Fee { get; set; }

		[Required]
		public int TrainingCourseId { get; set; }
		[ForeignKey("TrainingCourseId")]
		public TrainingCourse TrainingCourse { get; set; }

		public ICollection<RegisterClass>? RegisterClasses { get; set; }
		public ICollection<AssignClassTeaching>? AssignClassTeaches { get; set; }
		public ICollection<Score>? Scores { get; set; }
	}
}
