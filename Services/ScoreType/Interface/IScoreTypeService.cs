using SignupSystem.Models.DTO.ScoreType;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.ScoreType.Interface
{
	public interface IScoreTypeService
	{
		Task<ApiResponse<GetScoreTypesResponseDTO>> GetScoreTypesAsync();
		Task<ApiResponse<Models.ScoreType>> GetScoreTypeAsync(int scoreTypeId);
		ApiResponse<object> AddScoreTypeAsync(AddOrUpdateScoreTypeRequestDTO model);
		Task<ApiResponse<object>> UpdateScoreTypeAsync(int scoreTypeId, AddOrUpdateScoreTypeRequestDTO model);
		Task<ApiResponse<object>> DeleteScoreTypeAsync(int scoreTypeId);
	}
}
