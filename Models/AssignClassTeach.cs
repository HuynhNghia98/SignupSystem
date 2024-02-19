using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class AssignClassTeach
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string DayOfWeek { get; set; } = string.Empty;

		[Required]
		public TimeSpan StartTime { get; set; }

		[Required]
		public TimeSpan EndTime { get; set; }

		[Required]
		public DateTime StartDay { get; set; }

		[Required]
		public DateTime EndDay { get; set; }

		[Required]
		public DateTime CreateTime { get; set; } = DateTime.Now;

		public string? Details { get; set; } = string.Empty;

		[Required]
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public Class Class { get; set; }

		[Required]
		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public virtual Subject Subject { get; set; }
	}
}
