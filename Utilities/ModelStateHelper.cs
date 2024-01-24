using Microsoft.AspNetCore.Mvc.ModelBinding;

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
	}
}
