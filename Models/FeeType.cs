using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class FeeType
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string FeeTypeName { get; set; } = string.Empty;
		public string FeeTypeDetails { get; set; } = string.Empty;
		[Required]
		public ICollection<Payment>? Payments { get; set; }
	}
}
