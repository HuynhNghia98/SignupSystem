using SignupSystem.Models.DTO.Class;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Subject.Interfaces
{
	public interface ISubjectService
	{
		Task<ApiResponse<GetSubjectsResponseDTO>> GetSubjectsAsync();
		Task<ApiResponse<Models.Subject>> GetSubjectAsync(int subjectId);
		Task<ApiResponse<GetSubjectsResponseDTO>> SearchSubjectsAsync(string search);
		ApiResponse<object> AddSubjectAsync(AddOrUpdateSubjectRequestDTO model);
		Task<ApiResponse<object>> UpdateSubjectAsync(int subjectId, AddOrUpdateSubjectRequestDTO model);
		Task<ApiResponse<object>> DeleteSubjectAsync(int subjectId);
	}
}
