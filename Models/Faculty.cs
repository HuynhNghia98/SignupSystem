using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models
{
	public class Faculty
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		public string Details { get; set; } = string.Empty;
		public ICollection<Subject>? Subjects { get; set; }
	}
}
