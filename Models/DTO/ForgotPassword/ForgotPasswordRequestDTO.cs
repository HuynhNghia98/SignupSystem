using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.ForgotPassword
{
	public class ForgotPasswordRequestDTO
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
