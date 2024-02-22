using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Class
{
	public class StudentScore
	{
		[Required(ErrorMessage = "Nhập id học sinh")]
		public string StudentId { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập điểm cho học sinh")]
		public double Score { get; set; }
	}
}
