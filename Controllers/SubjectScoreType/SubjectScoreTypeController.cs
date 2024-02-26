using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.SubjectScoreType;
using SignupSystem.Services.SubjectScoreType.Interface;

namespace SignupSystem.Controllers.SubjectScoreType
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectScoreTypeController : ControllerBase
	{
		private ISubjectScoreTypeService _subjectScoreType;
		public SubjectScoreTypeController(ISubjectScoreTypeService subjectScoreType)
		{
			_subjectScoreType = subjectScoreType;
		}

		[HttpGet("GetSubjectScoreTypes")]
		public async Task<IActionResult> GetSubjectScoreTypes()
		{
			var result = await _subjectScoreType.GetSubjectScoreTypesAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpGet("GetSubjectScoreType/{id}")]
		public async Task<IActionResult> GetSubjectScoreType(int id)
		{
			var result = await _subjectScoreType.GetSubjectScoreTypeAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("SearchSubjectScoreType")]
		public async Task<IActionResult> SearchSubjectScoreType([FromForm] string search)
		{
			var result = await _subjectScoreType.SearchSubjectScoreTypesAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("AddSubjectScoreType")]
		public IActionResult AddSubjectScoreType([FromForm] AddOrUpdateSubjectScoreTypeRequestDTO model)
		{
			var result = _subjectScoreType.AddSubjectScoreTypeAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPut("UpdateSubjectScoreType/{id}")]
		public async Task<IActionResult> UpdateSubjectScoreType(int id, [FromForm] AddOrUpdateSubjectScoreTypeRequestDTO model)
		{
			var result = await _subjectScoreType.UpdateSubjectScoreTypeAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpDelete("DeleteSubjectScoreType/{id}")]
		public async Task<IActionResult> DeleteSubjectScoreType(int id)
		{
			var result = await _subjectScoreType.DeleteSubjectScoreTypeAsync(id);

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
