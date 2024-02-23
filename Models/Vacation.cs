using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class Vacation
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string VacationName { get; set; } = string.Empty;
		[Required]
		public string Reason { get; set; } = string.Empty;
		[Required]
		public DateTime StartDay { get; set; }

		[Required]
		public DateTime EndDay { get; set; }
	}
}
