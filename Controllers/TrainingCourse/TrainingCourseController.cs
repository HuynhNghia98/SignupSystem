﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Attributes;
using SignupSystem.Models.DTO.TrainingCourse;
using SignupSystem.Services.TrainningCourse.Interfaces;
using SignupSystem.Utilities;

namespace SignupSystem.Controllers.TrainingCourse
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class TrainingCourseController : ControllerBase
	{
		private ITrainingCourseService _trainingCourse;
		public TrainingCourseController(ITrainingCourseService trainingCourse)
		{
			_trainingCourse = trainingCourse;
		}

		[AuthorizeClaim(SD.Claim_ViewAllTrainingManagers)]
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
				return BadRequest(result);
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
				return BadRequest(result);
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
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteTrainingManager)]
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
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteTrainingManager)]
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
				return BadRequest(result);
			}

		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteTrainingManager)]
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
				return BadRequest(result);
			}

		}

		[HttpPost("CopyTrainingCourse")]
		public IActionResult CopyTrainingCourse([FromForm] CopyTraningCourseRequestDTO model)
		{
			var result = _trainingCourse.CopyTrainingCourseAsync(model);

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
