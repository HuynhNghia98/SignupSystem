using SignupSystem.Models.DTO.Lecturer;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;
using System.Threading.Tasks;

namespace SignupSystem.Services.Lecturer.Interfaces
{
	public interface ILecturerService
	{
		Task<ApiResponse<GetLecturersResponseDTO>> GetLecturersAsync();
		Task<ApiResponse<GetLecturerResponseDTO>> GetLecturerAsync(string id);
		Task<ApiResponse<GetLecturersResponseDTO>> SearchLecturersAsync(string search);
		Task<ApiResponse<object>> AddLecturerAsync(AddLecturerRequestDTO model);
		Task<ApiResponse<object>> UpdateLecturerAsync(string userId, UpdateLecturerRequestDTO model);
		Task<ApiResponse<object>> DeleteLecturerAsync(string userId);
		Task<ApiResponse<object>> GetTeachingScheduleAsync();
		Task<ApiResponse<GetClassesResponseDTO>> GetClassesAsync();
	}
}
