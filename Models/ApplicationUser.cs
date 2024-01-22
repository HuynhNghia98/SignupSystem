using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models
{
	public class ApplicationUser: IdentityUser
	{
		[Required]
		public string UserCode { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public int Gender { get; set; }
		[Required]
		public string Email { get; set; }
		public string? ImageUrl { get; set; }
		public string? Parents { get; set; }
		public string? Address { get; set; }
		[Required]
		public DateTime DOB { get; set; }
		public string? PhoneNumber { get; set; }
		public ICollection<RegisterCourse>? RegisterCourses { get; set; }
	}
}
