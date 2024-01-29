using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;
using SignupSystem.Services.Student.Interfaces;

namespace SignupSystem.Controllers.Student
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IStudentService _studentService;
		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}

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

		[HttpGet("GetClasses")]
		public async Task<IActionResult> GetClasses()
		{
			var result = await _studentService.GetClassesAsync();
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
		public IActionResult RegisterClass(string id, [FromForm] int classId)
		{
			var result = _studentService.RegisterClassForStudent(id, classId);
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
