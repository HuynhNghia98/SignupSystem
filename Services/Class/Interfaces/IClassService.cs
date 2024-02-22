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
		Task<ApiResponse<GetSubjectsResponseDTO>> GetSubjectListOfClassAsync(int classId);
		Task<ApiResponse<GetStudentsResponseDTO>> GetStudentListOfClassAsync(int classId);
		Task<ApiResponse<object>> AddScoreForStudentAsync(AddScoreForStudentRequestDTO model);
		Task<ApiResponse<object>> AddScoreForStudentsAsync(AddScoreForStudentsRequestDTO model);
		Task<ApiResponse<object>> UpdateScoreForStudentAsync(UpdateScoresForStudentRequestDTO model);
		Task<ApiResponse<GetScoreOfClassResponseDTO>> GetScoreOfClassAsync(int classId, int subjectId);
		Task<ApiResponse<object>> FinalizeStudentScoresAsync(int classId);
		Task<ApiResponse<GetScoresOfStudentResponseDTO>> GetScoresOfStudentAsync(int classId, string studentId);
	}
}
