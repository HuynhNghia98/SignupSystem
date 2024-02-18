using SignupSystem.Models.DTO.Department;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Department.Interfaces
{
	public interface IDepartmentService
	{
		Task<ApiResponse<GetDepartmentsResponseDTO>> GetDepartmentsAsync();
		Task<ApiResponse<Models.Department>> GetDepartmentAsync(int departmentId);
		Task<ApiResponse<GetDepartmentsResponseDTO>> SearchDepartmentsAsync(string search);
		ApiResponse<object> AddDepartmentAsync(AddOrUpdateDepartmentRequestDTO departmentId);
		Task<ApiResponse<object>> UpdateDepartmentAsync(int departmentId, AddOrUpdateDepartmentRequestDTO model);
		Task<ApiResponse<object>> DeleteDepartmentAsync(int departmentId);
	}
}
