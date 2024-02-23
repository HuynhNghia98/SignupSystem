using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.DTO.Vacation;
using SignupSystem.Services.Vacation.Interface;

namespace SignupSystem.Controllers.Vacation
{
	[Route("api/[controller]")]
	[ApiController]
	public class VacationController : ControllerBase
	{
		private IVacationService _vacation;
		public VacationController(IVacationService vacation)
		{
			_vacation = vacation;
		}

		[HttpGet("GetVacations")]
		public async Task<IActionResult> GetVacations()
		{
			var result = await _vacation.GetVacationsAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpGet("GetVacation/{id}")]
		public async Task<IActionResult> GetVacation(int id)
		{
			var result = await _vacation.GetVacationAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpGet("SearchVacation")]
		public async Task<IActionResult> SearchVacation([FromForm] string search)
		{
			var result = await _vacation.SearchVacationsAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpPost("AddVacation")]
		public IActionResult AddVacation([FromForm] AddOrUpdateVacationRequestDTO model)
		{
			var result = _vacation.AddVacationAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpPut("UpdateVacation/{id}")]
		public async Task<IActionResult> UpdateVacation(int id, [FromForm] AddOrUpdateVacationRequestDTO model)
		{
			var result = await _vacation.UpdateVacationAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpDelete("DeleteVacation/{id}")]
		public async Task<IActionResult> DeleteVacation(int id)
		{
			var result = await _vacation.DeleteVacationAsync(id);

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
