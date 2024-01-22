using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models
{
	public class Class
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public ICollection<RegisterCourse>? RegisterCourses { get; set; }
	}
}
