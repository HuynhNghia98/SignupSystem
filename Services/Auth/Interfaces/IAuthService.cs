using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.Response;

namespace SignupSystem.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> Login(LoginRequestDTO loginRequestDTO);
    }
}
