using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class AssignClassTeaching
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime CreateTime { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public Class Class { get; set; }
	}
}
