namespace SignupSystem.Models.DTO.Student
{
	public class GetStudentsResponseDTO
	{
		public GetStudentsResponseDTO()
		{
			Students = new();
		}

		public List<ApplicationUser> Students { get; set; }
	}
}
