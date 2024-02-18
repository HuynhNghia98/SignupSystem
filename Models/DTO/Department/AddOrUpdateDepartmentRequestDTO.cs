using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Department
{
	public class AddOrUpdateDepartmentRequestDTO
	{
		[Required(ErrorMessage = "Nhập tên tổ bộ môn")]
		public string DepartmentName { get; set; } = string.Empty;
	}
}
