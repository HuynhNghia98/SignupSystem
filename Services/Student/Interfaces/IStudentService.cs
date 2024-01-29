using SignupSystem.Models;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Student.Interfaces
{
	public interface IStudentService
	{
		Task<ApiResponse<GetStudentsResponseDTO>> GetStudentsAsync();
		Task<ApiResponse<GetStudentResponseDTO>> GetStudentAsync(string id);
		Task<ApiResponse<GetStudentsResponseDTO>> SearchStudentsAsync(string search);
		Task<ApiResponse<object>> UpdateStudentAsync();
		Task<ApiResponse<object>> AddStudentAsync(AddStudentRequestDTO model);
		Task<ApiResponse<object>> DeleteStudentAsync();
		Task<ApiResponse<object>> GetStudentClassesAsync();
		Task<ApiResponse<object>> PaySchoolFeeAsync();
		Task<ApiResponse<object>> GetStudentSchedulesAsync();
	}
}
