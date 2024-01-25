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
		private ApiResponse<object> _res;
		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
			_res = new();
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
