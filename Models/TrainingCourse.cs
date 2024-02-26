using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class TrainingCourse
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string TrainingCourseCode { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;
		public DateTime StartDate { get; set; } = DateTime.Now;
		public DateTime EndDate { get; set; } = DateTime.Now;
		public bool IsFinalizingSalary { get; set; } = false;
		public ICollection<Class>? Classes { get; set; }
		public ICollection<SubjectScoreType>? SubjectScoreTypes { get; set; }
		public ICollection<Salary>? Salaries { get; set; }
	}
}
