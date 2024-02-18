using SignupSystem.Models.DTO.Department;
using SignupSystem.Models.DTO.Faculty;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Faculty.Interfaces
{
	public interface IFacultyService
	{
		Task<ApiResponse<GetFacultiesResponseDTO>> GetFacultiesAsync();
		Task<ApiResponse<Models.Faculty>> GetFacultyAsync(int facultyId);
		Task<ApiResponse<GetFacultiesResponseDTO>> SearchFacultiesAsync(string search);
		ApiResponse<object> AddFacultyAsync(AddOrUpdateFacultyRequestDTO model);
		Task<ApiResponse<object>> UpdateFacultyAsync(int facultyId, AddOrUpdateFacultyRequestDTO model);
		Task<ApiResponse<object>> DeleteFacultyAsync(int facultyId);
	}
}
