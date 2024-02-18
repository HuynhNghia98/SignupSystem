using SignupSystem.Models.DTO.Class;
using SignupSystem.Models.DTO.Lecturer;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Class.Interfaces
{
    public interface IClassService
	{
		Task<ApiResponse<GetClassesResponseDTO>> GetClassesAsync();
		Task<ApiResponse<Models.Class>> GetClassAsync(int id);
		ApiResponse<object> AddClassAsync(AddOrUpdateClassRequestDTO model);
		Task<ApiResponse<object>> UpdateClassAsync(int classId, AddOrUpdateClassRequestDTO model);
		Task<ApiResponse<object>> DeleteClassAsync(int id);
	}
}
