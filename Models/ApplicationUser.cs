﻿using Microsoft.AspNetCore.Identity;
using SignupSystem.Utilities;
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
		[Required]
		public string PhoneNumber { get; set; } = string.Empty;
		public string? TaxCode { get; set; } = string.Empty;
		public bool? IsStudent { get; set; } = false;
		public bool? IsLecturer { get; set; } = false;
		public bool? IsEmployee { get; set; } = false;
		public string? ImageUrl { get; set; }
		public string? Parents { get; set; }
		public string? Address { get; set; }
		[Required]
		public DateTime DOB { get; set; }

		[NotMapped]
		public string Role { get; set; } = string.Empty;

		public string? SubjectTeaching { get; set; } = string.Empty;
		public ICollection<RegisterClass>? RegisterClasses { get; set; }
		public ICollection<AssignClassTeach>? AssignClassTeaches { get; set; }
		public ICollection<SubjectTeach>? SubjectTeaches { get; set; }
		public ICollection<Score>? Scores { get; set; }
		public ICollection<Salary>? Salaries { get; set; }
	}
}
