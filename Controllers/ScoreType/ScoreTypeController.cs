using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.ScoreType;
using SignupSystem.Services.ScoreType.Interface;

namespace SignupSystem.Controllers.ScoreType
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScoreTypeController : ControllerBase
	{
		private IScoreTypeService _scoreType;
		public ScoreTypeController(IScoreTypeService scoreType)
		{
			_scoreType = scoreType;
		}

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
				return BadRequest();
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
				return BadRequest();
			}

		}

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
				return BadRequest();
			}

		}

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
				return BadRequest();
			}

		}

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
				return BadRequest();
			}

		}
	}
}
