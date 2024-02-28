using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Attributes;
using SignupSystem.Models.DTO.Lecturer;
using SignupSystem.Services.Lecturer.Interfaces;
using SignupSystem.Utilities;

namespace SignupSystem.Controllers.Lecturer
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class LecturerController : ControllerBase
	{
		private readonly ILecturerService _lecturerService;

		public LecturerController(ILecturerService lecturerService)
		{
			_lecturerService = lecturerService;
		}

		[AuthorizeClaim(SD.Claim_ViewAllLecturers)]
		[HttpGet("GetLecturers")]
		public async Task<IActionResult> GetLecturers()
		{
			var result = await _lecturerService.GetLecturersAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpGet("GetLecturer/{id}")]
		public async Task<IActionResult> GetLecturer(string id)
		{
			var result = await _lecturerService.GetLecturerAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("SearchLecturers")]
		public async Task<IActionResult> SearchLecturers([FromForm] string search)
		{
			var result = await _lecturerService.SearchLecturersAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteLecturer)]
		[HttpPost("AddLecturer")]
		public async Task<IActionResult> AddLecturer([FromForm] AddLecturerRequestDTO model)
		{
			var result = await _lecturerService.AddLecturerAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteLecturer)]
		[HttpPut("UpdateLecturer/{id}")]
		public async Task<IActionResult> UpdateLecturer(string id, [FromForm] UpdateLecturerRequestDTO model)
		{
			var result = await _lecturerService.UpdateLecturerAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteLecturer)]
		[HttpDelete("DeleteLecturer/{id}")]
		public async Task<IActionResult> DeleteLecturer(string id)
		{
			var result = await _lecturerService.DeleteLecturerAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_ViewTeachingSchedule)]
		[HttpPost("GetAndSearchTeachingAssignmen")]
		public async Task<IActionResult> GetAndSearchTeachingAssignmen([FromForm] string? search, [FromForm] int classId)
		{
			var result = await _lecturerService.GetAndSearchTeachingAssignmentAsync(search, classId);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteTeachingSchedule)]
		[HttpPost("AddTeachingAssignment")]
		public async Task<IActionResult> AddTeachingAssignment([FromForm] AddTeachingAssignmentRequestDTO model)
		{
			var result = await _lecturerService.AddTeachingAssignmentAsync(model);

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
