using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class SubjectScoreType
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int ScoreColumn { get; set; }
		[Required]
		public int MandatoryScoreColumn { get; set; }


		[Required]
		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public Subject Subject { get; set; }

		[Required]
		public int TrainingCourseId { get; set; }
		[ForeignKey("TrainingCourseId")]
		public TrainingCourse TrainingCourse { get; set; }

		[Required]
		public int ScoreTypeId { get; set; }
		[ForeignKey("ScoreTypeId")]
		public ScoreType ScoreType { get; set; }
	}
}
