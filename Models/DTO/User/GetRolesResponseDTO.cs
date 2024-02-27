namespace SignupSystem.Models.DTO.User
{
	public class GetRolesResponseDTO
	{
		public GetRolesResponseDTO()
		{
			Roles = new();
		}
		public List<string> Roles { get; set; }
	}
}
