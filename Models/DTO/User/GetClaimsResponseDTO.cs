namespace SignupSystem.Models.DTO.User
{
	public class GetClaimsResponseDTO
	{
		public GetClaimsResponseDTO()
		{
			Claims = new();
		}
		public Dictionary<string, bool> Claims { get; set; }

	}
}
