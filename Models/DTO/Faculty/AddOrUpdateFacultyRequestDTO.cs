using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Faculty
{
	public class AddOrUpdateFacultyRequestDTO
	{
		[Required(ErrorMessage = "Nhập mã khoa")]
		public string FacultyCode { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập tên khoa")]
		public string FacultyName { get; set; } = string.Empty;
	}
}
