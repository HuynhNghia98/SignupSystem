﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Services.Auth.Interfaces;

namespace SignupSystem.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService) 
		{ 
			_authService = authService;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromForm] LoginRequestDTO loginRequestDTO)
		{
			var result = await _authService.Login(loginRequestDTO);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}
	}
}
