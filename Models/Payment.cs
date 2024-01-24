using SignupSystem.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class Payment
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime PaymentDate { get; set; }
		[Required]
		public string PaymentStatus { get; set; } = SD.Payment_UnPaid;
		[Required]
		public double Fee { get; set; }
		public double Discount { get; set; } = 0;
		public string? PaymentDetails { get; set; }

		[Required]
		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public Class Class { get; set; }

		[Required]
		public int FeeTypeId { get; set; }
		[ForeignKey("FeeTypeId")]
		public FeeType FeeType { get; set; }

		[Required]
		public ICollection<RegisterClass>? RegisterClasses { get; set; }
	}
}
