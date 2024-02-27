namespace SignupSystem.Models.DTO.User
{
	public class GetRoleClaimsResponseDTO
	{
		public GetRoleClaimsResponseDTO()
		{
			RoleClaims = new();
		}
		public Dictionary<string, bool> RoleClaims { get; set; }

	}
}
