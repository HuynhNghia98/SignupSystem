using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.ForgotPassword;
using SignupSystem.Models.Response;
using SignupSystem.Services.Auth.Interfaces;

namespace SignupSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ForgotPasswordController : ControllerBase
	{
		private readonly IForgotPasswordService _forgotPasswordService;
		public ApiResponse<object> _res;
		public ForgotPasswordController(IForgotPasswordService forgotPasswordService)
		{
			_forgotPasswordService = forgotPasswordService;
			_res = new();
		}

		[HttpPost("SendEmail")]
		public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordRequestDTO model)
		{
			var result = await _forgotPasswordService.ForgotPassword(model);
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
