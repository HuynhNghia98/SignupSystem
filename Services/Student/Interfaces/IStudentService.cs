using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Student.Interfaces
{
	public interface IStudentService
	{
		Task<ApiResponse<object>> GetStudentsAsync();
		Task<ApiResponse<object>> GetStudentAsync();
		Task<ApiResponse<object>> SearchStudentsAsync();
		Task<ApiResponse<object>> UpdateStudentAsync();
		Task<ApiResponse<object>> AddStudentAsync(AddStudentRequestDTO model);
		Task<ApiResponse<object>> DeleteStudentAsync();
		Task<ApiResponse<object>> GetStudentClassesAsync();
		Task<ApiResponse<object>> PaySchoolFeeAsync();
		Task<ApiResponse<object>> GetStudentSchedulesAsync();
	}
}
