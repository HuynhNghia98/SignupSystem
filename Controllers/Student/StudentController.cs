using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Attributes;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Services.Student.Interfaces;
using SignupSystem.Utilities;

namespace SignupSystem.Controllers.Student
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IStudentService _studentService;
		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}

		[AuthorizeClaim(SD.Claim_ViewAllStudents)]
		[HttpGet("GetStudents")]
		public async Task<IActionResult> GetStudents()
		{
			var result = await _studentService.GetStudentsAsync();
			if (result.IsSuccess)
			{

				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpGet("GetStudent/{id}")]
		public async Task<IActionResult> GetStudents(string id)
		{
			var result = await _studentService.GetStudentAsync(id);
			if (result.IsSuccess)
			{

				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("SearchStudents")]
		public async Task<IActionResult> SearchStudents([FromForm] string search)
		{
			var result = await _studentService.SearchStudentsAsync(search);
			if (result.IsSuccess)
			{

				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteStudent)]
		[HttpPost("AddStudent")]
		public async Task<IActionResult> AddStudent([FromForm] AddStudentRequestDTO model)
		{
			var result = await _studentService.AddStudentAsync(model);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteStudent)]
		[HttpPut("UpdateStudent/{id}")]
		public async Task<IActionResult> UpdateStudent(string id, [FromForm] UpdateStudentRequestDTO model)
		{
			var result = await _studentService.UpdateStudentAsync(id, model);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_AddEditDeleteStudent)]
		[HttpDelete("DeleteStudent/{id}")]
		public async Task<IActionResult> DeleteStudent(string id)
		{
			var result = await _studentService.DeleteStudentAsync(id);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpGet("GetStudentClasses/{id}")]
		public async Task<IActionResult> GetStudentClasses(string id)
		{
			var result = await _studentService.GetStudentClassesAsync(id);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[AuthorizeClaim(SD.Claim_CancelCourseRegistration)]
		[HttpDelete("DeleteRegisterdClasses/{id}")]
		public async Task<IActionResult> DeleteRegisterdClasses(int id)
		{
			var result = await _studentService.DeleteStudentRegisteredClassAsync(id);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("RegisterClass/{id}")]
		public async Task<IActionResult> RegisterClass(string id, [FromForm] int classId)
		{
			var result = await _studentService.RegisterClassForStudent(id, classId);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}


		[HttpPut("PaySchoolFee/{id}")]
		public async Task<IActionResult> PaySchoolFee(int id, [FromForm] PayFeeRequestDTO model)
		{
			var result = await _studentService.PaySchoolFeeAsync(id, model);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpGet("GetStudentSchedules/{id}")]
		public async Task<IActionResult> GetStudentSchedules(string id)
		{
			var result = await _studentService.GetStudentSchedulesAsync(id);
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
