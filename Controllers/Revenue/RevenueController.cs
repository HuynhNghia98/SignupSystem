using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Revenue;
using SignupSystem.Services.Revenue.Interface;

namespace SignupSystem.Controllers.Revenue
{
	[Route("api/[controller]")]
	[ApiController]
	public class RevenueController : ControllerBase
	{
		private readonly IRevenueService _revenue;
		public RevenueController(IRevenueService revenue)
		{
			_revenue = revenue;
		}

		[HttpPost("SearchStudentsHavePaidTuition")]
		public async Task<IActionResult> SearchStudentsHavePaidTuition([FromForm] string? search, [FromForm] int trainingCourseId)
		{
			var result = await _revenue.SearchStudentsHavePaidTuitionAsync(search, trainingCourseId);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("CalculateEmployeeSalary")]
		public async Task<IActionResult> CalculateEmployeeSalary([FromForm] CalculateEmployeeSalaryRequestDTO model)
		{
			var result = await _revenue.CalculateEmployeeSalaryAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("AddEmployeeSalary/{id}")]
		public async Task<IActionResult> AddEmployeeSalary(string id, [FromForm] AddCalculateEmployeeSalaryRequestDTO model)
		{
			var result = await _revenue.AddEmployeeSalaryAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}

		}

		[HttpPost("FinalizingSalariesForEmployees")]
		public async Task<IActionResult> FinalizingSalariesForEmployees([FromForm] int trainingCourseId)
		{
			var result = await _revenue.FinalizingSalariesForEmployeesAsync(trainingCourseId);

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
