﻿using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.ForgotPassword
{
	public class ChangePasswordRequestDTO
	{
		[Required]
		public string UserId { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string ResetToken { get; set; }
	}
}
