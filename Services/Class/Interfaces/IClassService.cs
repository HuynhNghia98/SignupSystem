using SignupSystem.Models.DTO.Class;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Class.Interfaces
{
    public interface IClassService
	{
		Task<ApiResponse<GetClassesResponseDTO>> GetClassesAsync();
		Task<ApiResponse<Models.Class>> GetClassAsync(int id);
		Task<ApiResponse<GetClassesResponseDTO>> SearchClassesAsync(string searchById, string searchByName);
		ApiResponse<object> AddClassAsync(AddOrUpdateClassRequestDTO model);
		Task<ApiResponse<object>> UpdateClassAsync(int classId, AddOrUpdateClassRequestDTO model);
		Task<ApiResponse<object>> DeleteClassAsync(int id);

		//new
		Task<ApiResponse<GetSubjectsResponseDTO>> GetSubjectListOfClassAsync(int classId);
		Task<ApiResponse<GetStudentsResponseDTO>> GetStudentListOfClassAsync(int classId);
		ApiResponse<object> AddScoreForClassAsync(int classId, string studentId);
		Task<ApiResponse<GetStudentsResponseDTO>> GetScoreOfClassAsync(int classId);
	}
}
