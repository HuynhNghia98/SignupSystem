using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models
{
	public class Schedule
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string DayOfWeek { get; set; } = string.Empty;
		[Required]
		public TimeSpan StartTime { get; set; }
		[Required]
		public TimeSpan EndTime { get; set; }
		public string ScheduleDetails { get; set; } = string.Empty;

		public ICollection<RegisterSchedule>? RegisterSchedules { get; set; }
	}
}
