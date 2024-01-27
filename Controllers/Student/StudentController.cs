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
	}
}
