using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class SubjectTeach
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public Subject Subject { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }
	}
}
