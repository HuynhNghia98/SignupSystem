using SignupSystem.Models.DTO.Vacation;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Vacation.Interface
{
	public interface IVacationService
	{
		Task<ApiResponse<GetVacationsResponseDTO>> GetVacationsAsync();
		Task<ApiResponse<Models.Vacation>> GetVacationAsync(int vacationId);
		Task<ApiResponse<GetVacationsResponseDTO>> SearchVacationsAsync(string search);
		ApiResponse<object> AddVacationAsync(AddOrUpdateVacationRequestDTO model);
		Task<ApiResponse<object>> UpdateVacationAsync(int vacationId, AddOrUpdateVacationRequestDTO model);
		Task<ApiResponse<object>> DeleteVacationAsync(int vacationId);
	}
}
