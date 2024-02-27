using Microsoft.AspNetCore.Mvc.ModelBinding;
using SignupSystem.Models.DTO.Auth;

namespace SignupSystem.Utilities
{
	public static class ModelStateHelper
	{
		public static void AddModelError<TModel>(ModelStateDictionary modelState, string propertyName, string errorMessage)
		{
			modelState.AddModelError(propertyName, errorMessage);
		}

		public static Dictionary<string, List<string>> ConvertToDictionary(ModelStateDictionary modelState)
		{
			return modelState.ToDictionary(
				kvp => kvp.Key,
				kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
			);
		}

		//ModelStateHelper.AddModelError<AddOrUpdateUserRequestDTO>(ModelState, nameof(AddOrUpdateUserRequestDTO.RoleId), "Vai trò không tồn tại.");
		//_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
		//_res.IsSuccess = false;


		//_res.Errors = new Dictionary<string, List<string>>
		//{
		//	{ nameof(LoginRequestDTO.Username), new List<string> { $"Không thể đăng nhập." }}
		//};
	}
}
