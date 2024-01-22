using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.ForgotPassword;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Auth.Interfaces
{
	public interface IForgotPasswordService
	{
		Task<ApiResponse<object>> ForgotPassword(ForgotPasswordRequestDTO model);
	}
}
