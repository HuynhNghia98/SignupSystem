using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Department;
using SignupSystem.Models.DTO.Faculty;
using SignupSystem.Services.Department.Interfaces;
using SignupSystem.Services.Faculty.Interfaces;

namespace SignupSystem.Controllers.Faculty
{
	[Route("api/[controller]")]
	[ApiController]
	public class FacultyController : ControllerBase
	{
		private IFacultyService _faculty;
		public FacultyController(IFacultyService faculty)
		{
			_faculty = faculty;
		}

		[HttpGet("GetFaculties")]
		public async Task<IActionResult> GetFaculties()
		{
			var result = await _faculty.GetFacultiesAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpGet("GetFaculty/{id}")]
		public async Task<IActionResult> GetFaculty(int id)
		{
			var result = await _faculty.GetFacultyAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("SearchFaculty")]
		public async Task<IActionResult> SearchFaculty([FromForm] string search)
		{
			var result = await _faculty.SearchFacultiesAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("AddFaculty")]
		public IActionResult AddFaculty([FromForm] AddOrUpdateFacultyRequestDTO model)
		{
			var result = _faculty.AddFacultyAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPut("UpdateFaculty/{id}")]
		public async Task<IActionResult> UpdateFaculty(int id, [FromForm] AddOrUpdateFacultyRequestDTO model)
		{
			var result = await _faculty.UpdateFacultyAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpDelete("DeleteFaculty/{id}")]
		public async Task<IActionResult> DeleteFaculty(int id)
		{
			var result = await _faculty.DeleteFacultyAsync(id);

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
