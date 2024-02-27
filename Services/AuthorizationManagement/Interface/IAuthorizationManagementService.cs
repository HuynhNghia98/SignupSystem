using SignupSystem.Models.DTO.User;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.AuthorizationManagement.Interface
{
	public interface IAuthorizationManagementService
	{
		Task<ApiResponse<GetUsersResponseDTO>> GetAndSearchUsersAsync(string? search);
		Task<ApiResponse<object>> AddUserAsync(AddOrUpdateUserRequestDTO model);
		Task<ApiResponse<object>> UpdateUserAsync(string userId, AddOrUpdateUserRequestDTO model);
		Task<ApiResponse<object>> DeleteUserAsync(string userId);
		//
		ApiResponse<GetRolesResponseDTO> GetRoles();
		Task<ApiResponse<GetRoleClaimsResponseDTO>> GetRoleClaimsAsync(string roleName);
		Task<ApiResponse<object>> UpdateRoleClaimsAsync(UpdateRoleClaimRequestDTO model);
	}
}
