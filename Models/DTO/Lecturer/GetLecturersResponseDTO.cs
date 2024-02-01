namespace SignupSystem.Models.DTO.Lecturer
{
	public class GetLecturersResponseDTO
	{
		public GetLecturersResponseDTO()
		{
			Lecturers = new();
		}
		public List<ApplicationUser> Lecturers { get; set; }
	}
}
