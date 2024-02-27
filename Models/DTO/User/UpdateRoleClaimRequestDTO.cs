using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.User
{
	public class UpdateRoleClaimRequestDTO
	{
		[Required(ErrorMessage ="Nhập vai trò")]
		public string RoleName { get; set; } = string.Empty;
		[Required]
		public Dictionary<string, bool> RoleClaims { get; set; } = new();
	}
}
