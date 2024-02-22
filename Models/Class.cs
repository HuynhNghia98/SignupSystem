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
		public double Fee { get; set; }
		[Required]
		public int StudentQuantity { get; set; }
		[Required]
		public bool Status { get; set; }
		[Required]
		public bool FinalizeStudentScores { get; set; } = false;
		public string? ImageUrl { get; set; }
		public string? Detail { get; set; } = string.Empty;



		[Required]
		public int FacultyId { get; set; }
		[ForeignKey("FacultyId")]
		public Faculty Faculty { get; set; }

		[Required]
		public int TrainingCourseId { get; set; }
		[ForeignKey("TrainingCourseId")]
		public TrainingCourse TrainingCourse { get; set; }

		public ICollection<RegisterClass>? RegisterClasses { get; set; }
		public ICollection<AssignClassTeach>? AssignClassTeaches { get; set; }
		public ICollection<Score>? Scores { get; set; }
	}
}
