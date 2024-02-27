namespace SignupSystem.Models.DTO.User
{
	public class GetUsersResponseDTO
	{
		public GetUsersResponseDTO()
		{
			Users = new();
		}
		public List<ApplicationUser> Users { get; set; }
	}
}
