using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.DTO.TrainingCourse;
using SignupSystem.Services.Subject.Interfaces;
using SignupSystem.Services.TrainningCourse.Interfaces;

namespace SignupSystem.Controllers.TrainingCourse
{
	[Route("api/[controller]")]
	[ApiController]
	public class TrainingCourseController : ControllerBase
	{
		private ITrainingCourseService _trainingCourse;
		public TrainingCourseController(ITrainingCourseService trainingCourse)
		{
			_trainingCourse = trainingCourse;
		}

		[HttpGet("GetTrainingCourses")]
		public async Task<IActionResult> GetTrainingCourses()
		{
			var result = await _trainingCourse.GetTrainingCoursesAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpGet("GetTrainingCourse/{id}")]
		public async Task<IActionResult> GetTrainingCourse(int id)
		{
			var result = await _trainingCourse.GetTrainingCourseAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpPost("SearchTrainingCourse")]
		public async Task<IActionResult> SearchTrainingCourse([FromForm] string search)
		{
			var result = await _trainingCourse.SearchTrainingCourseAsync(search);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpPost("AddTrainingCourse")]
		public IActionResult AddTrainingCourse([FromForm] AddOrUpdateTrainingCourseRequestDTO model)
		{
			var result = _trainingCourse.AddTrainingCourseAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpPut("UpdateTrainingCourse/{id}")]
		public async Task<IActionResult> UpdateTrainingCourse(int id, [FromForm] AddOrUpdateTrainingCourseRequestDTO model)
		{
			var result = await _trainingCourse.UpdateTrainingCourseAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpDelete("DeleteTrainingCourse/{id}")]
		public async Task<IActionResult> DeleteTrainingCourse(int id)
		{
			var result = await _trainingCourse.DeleteTrainingCourseAsync(id);

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
