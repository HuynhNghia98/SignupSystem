using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Student
{
	public class AddStudentRequestDTO
	{
		[Required]
		public string LastName { get; set; } = string.Empty;
		[Required]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		public string DOB { get; set; } = string.Empty;
		[Required]
		public int Gender { get; set; }
		[Required]
		public string Email { get; set; } = string.Empty;
		public string? PhoneNumer { get; set; } = string.Empty;
		public string? Address { get; set; } = string.Empty;
		public string? Parents { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public int ClassId { get; set; }
	}
}
