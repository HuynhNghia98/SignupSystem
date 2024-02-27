using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.User;
using SignupSystem.Services.AuthorizationManagement.Interface;

namespace SignupSystem.Controllers.AuthorizationManagement
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationManagementController : ControllerBase
	{
		private IAuthorizationManagementService _authorizationManagement;
		public AuthorizationManagementController(IAuthorizationManagementService authorizationManagement)
		{
			_authorizationManagement = authorizationManagement;
		}

		[HttpPost("GetAndSearchUsers")]
		public async Task<IActionResult> GetAndSearchUsers([FromForm] string? search)
		{
			var result = await _authorizationManagement.GetAndSearchUsersAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("AddUser")]
		public async Task<IActionResult> AddUser([FromForm] AddOrUpdateUserRequestDTO model)
		{
			var result = await _authorizationManagement.AddUserAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("UpdateUser/{userId}")]
		public async Task<IActionResult> UpdateUser(string userId, [FromForm] AddOrUpdateUserRequestDTO model)
		{
			var result = await _authorizationManagement.UpdateUserAsync(userId, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("DeleteUser/{userId}")]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			var result = await _authorizationManagement.DeleteUserAsync(userId);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("GetRoles")]
		public IActionResult GetRoles()
		{
			var result = _authorizationManagement.GetRoles();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("GetRoleClaims")]
		public async Task<IActionResult> GetRoleClaims([FromForm] string roleName)
		{
			var result = await _authorizationManagement.GetRoleClaimsAsync(roleName);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("UpdateRoleClaims")]
		public async Task<IActionResult> UpdateRoleClaims([FromBody] UpdateRoleClaimRequestDTO model)
		{
			var result = await _authorizationManagement.UpdateRoleClaimsAsync(model);

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
