namespace SignupSystem.Models.DTO.Lecturer
{
	public class GetLecturerResponseDTO
	{
		public GetLecturerResponseDTO()
		{
			Lecturer = new();
		}
		public ApplicationUser Lecturer { get; set; }
	}
}
