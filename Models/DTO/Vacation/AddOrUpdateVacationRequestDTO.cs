using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Vacation
{
	public class AddOrUpdateVacationRequestDTO
	{
		[Required(ErrorMessage = "Nhập tên kỳ nghỉ")]
		public string VacationName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập lý do")]
		public string Reason { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập ngày bắt đầu")]
		public DateTime StartDay { get; set; }
		[Required(ErrorMessage = "Nhập ngày kết thúc")]
		public DateTime EndDay { get; set; }
	}
}
