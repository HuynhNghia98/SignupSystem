using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Student
{
	public class PayFeeRequestDTO
	{
		[Required(ErrorMessage = "Cần chọn loại phí.")]
		public int FeeTypeId { get; set; }
		[Required(ErrorMessage = "Cần nhập mức phí.")]
		public double Fee { get; set; }
		public double? Discount { get; set; }
		public string? Notes { get; set; }
	}
}
