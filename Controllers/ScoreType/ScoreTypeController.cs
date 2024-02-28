using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Attributes;
using SignupSystem.Models.DTO.ScoreType;
using SignupSystem.Services.ScoreType.Interface;
using SignupSystem.Utilities;

namespace SignupSystem.Controllers.ScoreType
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ScoreTypeController : ControllerBase
	{
		private IScoreTypeService _scoreType;
		public ScoreTypeController(IScoreTypeService scoreType)
		{
			_scoreType = scoreType;
		}

		[AuthorizeClaim(SD.Claim_ViewAllScoreTypes)]
		[HttpGet("GetScoreTypes")]
		public async Task<IActionResult> GetScoreTypes()
		{
			var result = await _scoreType.GetScoreTypesAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpGet("GetScoreType/{id}")]
		public async Task<IActionResult> GetScoreType(int id)
		{
			var result = await _scoreType.GetScoreTypeAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteScoreType)]
		[HttpPost("AddScoreType")]
		public IActionResult AddScoreType([FromForm] AddOrUpdateScoreTypeRequestDTO model)
		{
			var result = _scoreType.AddScoreTypeAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteScoreType)]
		[HttpPut("UpdateScoreType/{id}")]
		public async Task<IActionResult> UpdateScoreType(int id, [FromForm] AddOrUpdateScoreTypeRequestDTO model)
		{
			var result = await _scoreType.UpdateScoreTypeAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteScoreType)]
		[HttpDelete("DeleteScoreType/{id}")]
		public async Task<IActionResult> DeleteScoreType(int id)
		{
			var result = await _scoreType.DeleteScoreTypeAsync(id);

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
