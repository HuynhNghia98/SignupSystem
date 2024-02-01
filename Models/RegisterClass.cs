using SignupSystem.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class RegisterClass
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime CreateTime { get; set; } = DateTime.Now;
		public DateTime PaymentDate { get; set; }
		public string PaymentStatus { get; set; }
		[Required]
		public double Fee { get; set; }
		public double? Discount { get; set; }
		public string? PaymentDetails { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public Class Class { get; set; }

		[Required]
		public int FeeTypeId { get; set; }
		[ForeignKey("FeeTypeId")]
		public FeeType FeeType { get; set; }
	}
}
