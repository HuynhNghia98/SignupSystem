using Microsoft.AspNetCore.Mvc;
using SignupSystem.DataAccess.Data;
using SignupSystem.Models.DTO.User;
using SignupSystem.Services.AuthorizationManagement.Interface;
using SignupSystem.Utilities;
using SignupSystem.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace SignupSystem.Controllers.AuthorizationManagement
{
	[Authorize(Roles = SD.Role_Admin)]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationManagementController : ControllerBase
	{
		private IAuthorizationManagementService _authorizationManagement;
		public AuthorizationManagementController(IAuthorizationManagementService authorizationManagement)
		{
			_authorizationManagement = authorizationManagement;
		}

		[AuthorizeClaim(SD.Claim_ViewUserList)]
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

		[HttpGet("GetUser/{userId}")]
		public async Task<IActionResult> GetUser(string userId)
		{
			var result = await _authorizationManagement.GetUserAsync(userId);

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

		[HttpPut("UpdateUser/{userId}")]
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

		[HttpDelete("DeleteUser/{userId}")]
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

		
		[HttpGet("GetRoles")]
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

		[AuthorizeClaim(SD.Claim_AddEditDeleteUserRole)]
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

		[AuthorizeClaim(SD.Claim_AddEditDeleteUserRole)]
		[Authorize(Policy = "IsAdminClaimAccess")]
		[HttpPut("UpdateRoleClaims")]
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

		[AuthorizeClaim(SD.Claim_AddEditDeleteUserRole)]
		[HttpGet("GetUserClaims/{userId}")]
		public async Task<IActionResult> GetUserClaims(string userId)
		{
			var result = await _authorizationManagement.GetUserClaimsAsync(userId);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteUserRole)]
		[HttpPut("UpdateUserClaims/{userId}")]
		public async Task<IActionResult> UpdateUserClaims(string userId, [FromBody] UpdateUserClaimsRequestDTO model)
		{
			var result = await _authorizationManagement.UpdateUserClaimsAsync(userId, model);

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
