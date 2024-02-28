using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Attributes;
using SignupSystem.Models.DTO.Class;
using SignupSystem.Services.Class.Interfaces;
using SignupSystem.Utilities;

namespace SignupSystem.Controllers.Class
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ClassController : ControllerBase
	{
		private IClassService _classService;
		public ClassController(IClassService classService)
		{
			_classService = classService;
		}

		[AuthorizeClaim(SD.Claim_ViewClassList)]
		[HttpGet("GetClasses")]
		public async Task<IActionResult> GetClasses()
		{
			var result = await _classService.GetClassesAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpGet("GetClass/{id}")]
		public async Task<IActionResult> GetClass(int id)
		{
			var result = await _classService.GetClassAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("SearchClass")]
		public async Task<IActionResult> SearchClass([FromForm] string? searchById, [FromForm] string? searchByName)
		{
			if (searchById == null)
			{
				searchById = "";
			}
			if (searchByName == null)
			{
				searchByName = "";
			}

			var result = await _classService.SearchClassesAsync(searchById, searchByName);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteClass)]
		[HttpPost("AddClass")]
		public IActionResult AddClass([FromForm] AddOrUpdateClassRequestDTO model)
		{
			var result = _classService.AddClassAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteClass)]
		[HttpPut("UpdateClass/{id}")]
		public async Task<IActionResult> UpdateClass(int id, [FromForm] AddOrUpdateClassRequestDTO model)
		{
			var result = await _classService.UpdateClassAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteClass)]
		[HttpDelete("DeleteClass/{id}")]
		public async Task<IActionResult> DeleteClass(int id)
		{
			var result = await _classService.DeleteClassAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_ViewSubjectListInClass)]
		[HttpGet("GetSubjectListOfClass/{id}")]
		public async Task<IActionResult> GetSubjectListOfClass(int id)
		{
			var result = await _classService.GetSubjectListOfClassAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_ViewStudentListInClass)]
		[HttpGet("GetStudentListOfClass/{id}")]
		public async Task<IActionResult> GetStudentListOfClass(int id)
		{
			var result = await _classService.GetStudentListOfClassAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("AddScoreForStudent")]
		public async Task<IActionResult> AddScoreForStudent([FromForm] AddScoreForStudentRequestDTO model)
		{
			var result = await _classService.AddScoreForStudentAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("AddScoreForStudents")]
		public async Task<IActionResult> AddScoreForStudents([FromBody] AddScoreForStudentsRequestDTO model)
		{
			var result = await _classService.AddScoreForStudentsAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_UpdateScores)]
		[HttpPut("UpdateScoreForStudent")]
		public async Task<IActionResult> UpdateScoreForStudent([FromBody] UpdateScoresForStudentRequestDTO model)
		{
			var result = await _classService.UpdateScoreForStudentAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_ViewScores)]
		[HttpPost("GetScoreOfClass")]
		public async Task<IActionResult> GetScoreOfClass([FromForm] int classId, [FromForm] int subjectId)
		{
			var result = await _classService.GetScoreOfClassAsync(classId, subjectId);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPut("FinalizeStudentScores")]
		public async Task<IActionResult> FinalizeStudentScores([FromForm] int classId)
		{
			var result = await _classService.FinalizeStudentScoresAsync(classId);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("GetScoresOfStudent")]
		public async Task<IActionResult> GetScoresOfStudent([FromForm] int classId, [FromForm] string studentId)
		{
			var result = await _classService.GetScoresOfStudentAsync(classId, studentId);

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
