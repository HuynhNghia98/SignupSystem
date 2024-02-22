using SignupSystem.Models;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.DTO.Class;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Student.Interfaces
{
    public interface IStudentService
	{
		Task<ApiResponse<GetStudentsResponseDTO>> GetStudentsAsync();
		Task<ApiResponse<GetStudentResponseDTO>> GetStudentAsync(string id);
		Task<ApiResponse<GetStudentsResponseDTO>> SearchStudentsAsync(string search);
		Task<ApiResponse<object>> UpdateStudentAsync(string id, UpdateStudentRequestDTO model);
		Task<ApiResponse<object>> AddStudentAsync(AddStudentRequestDTO model);
		Task<ApiResponse<object>> DeleteStudentAsync(string id);
		Task<ApiResponse<GetStudentClassesResponseDTO>> GetStudentClassesAsync(string id);
		Task<ApiResponse<object>> DeleteStudentRegisteredClassAsync(int id);
		Task<ApiResponse<object>> PaySchoolFeeAsync(int id, PayFeeRequestDTO model);
		Task<ApiResponse<GetStudentSchedulesResponseDTO>> GetStudentSchedulesAsync(string studentId);
		ApiResponse<object> RegisterClassForStudent(string userId,int classId);
	}
}
