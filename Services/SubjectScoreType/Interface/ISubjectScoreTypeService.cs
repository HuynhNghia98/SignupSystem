using SignupSystem.Models.DTO.Faculty;
using SignupSystem.Models.DTO.SubjectScoreType;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.SubjectScoreType.Interface
{
	public interface ISubjectScoreTypeService
	{
		Task<ApiResponse<GetSubjectScoreTypeResponseDTO>> GetSubjectScoreTypesAsync();
		Task<ApiResponse<Models.SubjectScoreType>> GetSubjectScoreTypeAsync(int id);
		Task<ApiResponse<GetSubjectScoreTypeResponseDTO>> SearchSubjectScoreTypesAsync(string search);
		ApiResponse<object> AddSubjectScoreTypeAsync(AddOrUpdateSubjectScoreTypeRequestDTO model);
		Task<ApiResponse<object>> UpdateSubjectScoreTypeAsync(int id, AddOrUpdateSubjectScoreTypeRequestDTO model);
		Task<ApiResponse<object>> DeleteSubjectScoreTypeAsync(int id);
	}
}
