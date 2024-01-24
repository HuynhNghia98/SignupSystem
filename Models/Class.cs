using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models
{
	public class Class
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public double Fee { get; set; } 

		public ICollection<RegisterClass>? RegisterClasses { get; set; }
		public ICollection<AssignClassTeaching>? AssignClassTeachings { get; set; }
		public ICollection<RegisterSchedule>? RegisterSchedules { get; set; }
		public ICollection<Payment>? Payments { get; set; }
	}
}
