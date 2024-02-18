using SignupSystem.Models;
using SignupSystem.Models.DTO.TrainingCourse;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.TrainningCourse.Interfaces
{
	public interface ITrainingCourseService
	{
		Task<ApiResponse<GetTrainingCoursesResponseDTO>> GetTrainingCoursesAsync();
		Task<ApiResponse<TrainingCourse>> GetTrainingCourseAsync(int id);
		Task<ApiResponse<GetTrainingCoursesResponseDTO>> SearchTrainingCourseAsync(string search);
		ApiResponse<object> AddTrainingCourseAsync(AddOrUpdateTrainingCourseRequestDTO model);
		Task<ApiResponse<object>> UpdateTrainingCourseAsync(int id, AddOrUpdateTrainingCourseRequestDTO model);
		Task<ApiResponse<object>> DeleteTrainingCourseAsync(int id);
		Task<ApiResponse<object>> CopyTrainingCourseAsync(int id);
	}
}
