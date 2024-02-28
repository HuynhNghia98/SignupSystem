using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Services.Subject.Interfaces;

namespace SignupSystem.Controllers.Subject
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		private ISubjectService _subjectService;
		public SubjectController(ISubjectService subjectService)
		{
			_subjectService = subjectService;
		}

		[HttpGet("GetSubjects")]
		public async Task<IActionResult> GetSubjects()
		{
			var result = await _subjectService.GetSubjectsAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpGet("GetSubject/{id}")]
		public async Task<IActionResult> GetSubject(int id)
		{
			var result = await _subjectService.GetSubjectAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpGet("SearchSubject")]
		public async Task<IActionResult> SearchSubject([FromForm]string search)
		{
			var result = await _subjectService.SearchSubjectsAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("AddSubject")]
		public IActionResult AddSubject([FromForm] AddOrUpdateSubjectRequestDTO model)
		{
			var result = _subjectService.AddSubjectAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPut("UpdateSubject/{id}")]
		public async Task<IActionResult> UpdateSubject(int id, [FromForm] AddOrUpdateSubjectRequestDTO model)
		{
			var result = await _subjectService.UpdateSubjectAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpDelete("DeleteSubject/{id}")]
		public async Task<IActionResult> DeleteSubject(int id)
		{
			var result = await _subjectService.DeleteSubjectAsync(id);

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
