using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string SubjectCode { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;
		public string? Details { get; set; } = string.Empty;

		[Required]
		public int FacultyId { get; set; }
		[ForeignKey("FacultyId")]
		public Faculty Faculty { get; set; }

		[Required]
		public int DepartmentId { get; set; }
		[ForeignKey("DepartmentId")]
		public Department Department { get; set; }

		public ICollection<SubjectTeach>? SubjectTeaches { get; set; }
	}
}
