using SignupSystem.Models.DTO.User;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.AuthorizationManagement.Interface
{
	public interface IAuthorizationManagementService
	{
		Task<ApiResponse<GetUsersResponseDTO>> GetAndSearchUsersAsync(string? search);
		Task<ApiResponse<Models.ApplicationUser>> GetUserAsync(string userId);
		Task<ApiResponse<object>> AddUserAsync(AddUserRequestDTO model);
		Task<ApiResponse<object>> UpdateUserAsync(string userId, UpdateUserRequestDTO model);
		Task<ApiResponse<object>> DeleteUserAsync(string userId);
		//
		ApiResponse<GetRolesResponseDTO> GetRoles();
		Task<ApiResponse<GetClaimsResponseDTO>> GetRoleClaimsAsync(string roleName);
		Task<ApiResponse<object>> UpdateRoleClaimsAsync(UpdateRoleClaimRequestDTO model);

		//
		Task<ApiResponse<GetClaimsResponseDTO>> GetUserClaimsAsync(string userId);
		Task<ApiResponse<object>> UpdateUserClaimsAsync(string userId, UpdateUserClaimsRequestDTO model);
	}
}
