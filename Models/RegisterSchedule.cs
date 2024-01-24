using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class RegisterSchedule
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime Createtime { get; set; }

		[Required]
		public int ScheduleId { get; set; }
		[ForeignKey("ScheduleId")]
		public Schedule Schedule { get; set; }

		[Required]
		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public Class Class { get; set; }
	}
}
