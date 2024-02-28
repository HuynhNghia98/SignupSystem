using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.User
{
	public class UpdateUserClaimsRequestDTO
	{
		[Required]
		public Dictionary<string, bool> newUserClaims { get; set; } = new();
	}
}
