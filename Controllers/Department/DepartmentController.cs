using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Department;
using SignupSystem.Services.Department.Interfaces;

namespace SignupSystem.Controllers.Department
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private IDepartmentService _department;
		public DepartmentController(IDepartmentService department)
		{
			_department = department;
		}

		[HttpGet("GetDepartments")]
		public async Task<IActionResult> GetDepartments()
		{
			var result = await _department.GetDepartmentsAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpGet("GetDepartment/{id}")]
		public async Task<IActionResult> GetDepartment(int id)
		{
			var result = await _department.GetDepartmentAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("SearchDepartment")]
		public async Task<IActionResult> SearchDepartment([FromForm] string search)
		{
			var result = await _department.SearchDepartmentsAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("AddDepartment")]
		public IActionResult AddDepartment([FromForm] AddOrUpdateDepartmentRequestDTO model)
		{
			var result = _department.AddDepartmentAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPut("UpdateDepartment/{id}")]
		public async Task<IActionResult> UpdateDepartment(int id, [FromForm] AddOrUpdateDepartmentRequestDTO model)
		{
			var result = await _department.UpdateDepartmentAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpDelete("DeleteDepartment/{id}")]
		public async Task<IActionResult> DeleteDepartment(int id)
		{
			var result = await _department.DeleteDepartmentAsync(id);

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
