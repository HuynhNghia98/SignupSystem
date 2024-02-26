using SignupSystem.Models.DTO.Lecturer;
using SignupSystem.Models.DTO.Revenue;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Revenue.Interface
{
	public interface IRevenueService
	{
		Task<ApiResponse<GetStudentsResponseDTO>> SearchStudentsHavePaidTuitionAsync(string? search, int trainingCourseId);
		Task<ApiResponse<GetSalaryResponseDTO>> CalculateEmployeeSalaryAsync(CalculateEmployeeSalaryRequestDTO model);
		Task<ApiResponse<object>> AddEmployeeSalaryAsync(string lecturerId, AddCalculateEmployeeSalaryRequestDTO model);
		Task<ApiResponse<object>> FinalizingSalariesForEmployeesAsync(int trainingCourseId);
	}
}
