using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Class;
using SignupSystem.Services.Class.Interfaces;

namespace SignupSystem.Controllers.Class
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassController : ControllerBase
	{
		private IClassService _classService;
		public ClassController(IClassService classService)
		{
			_classService = classService;
		}

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
				return BadRequest();
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
				return BadRequest();
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
				return BadRequest();
			}

		}

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
				return BadRequest();
			}

		}

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
				return BadRequest();
			}

		}

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
				return BadRequest();
			}

		}
	}
}
