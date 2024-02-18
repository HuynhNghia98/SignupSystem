using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class Score
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public double ScoreOfStudent { get; set; }
		public DateTime Creatime { get; set; } = DateTime.Now;

		[Required]
		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public Subject Subject { get; set; }

		[Required]
		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public Class Class { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public int ScoreTypeId { get; set; }
		[ForeignKey("ScoreTypeId")]
		public ScoreType ScoreType { get; set; }
	}
}
