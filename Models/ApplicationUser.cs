using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string UserCode { get; set; } = string.Empty;
		[Required]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		public string LastName { get; set; } = string.Empty;
		[Required]
		public int Gender { get; set; }
		[Required]
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string TaxCode { get; set; } = string.Empty;
		public bool IsStudent { get; set; } = false;
		public bool IsLecturer { get; set; } = false;
		public bool IsAdmin { get; set; } = false;
		public string? ImageUrl { get; set; }
		public string? Parents { get; set; }
		public string? Address { get; set; }
		[Required]
		public DateTime DOB { get; set; }

		[Required]
		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public Subject Subject { get; set; }

		public ICollection<RegisterClass>? RegisterClasses { get; set; }
	}
}
