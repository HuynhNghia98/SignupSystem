using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Class
{
	public class AddOrUpdateClassRequestDTO
	{
		[Required(ErrorMessage = "Nhập tên lớp")]
		public string ClassName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập học phí lớp")]
		public double Fee { get; set; }

	}
}
