namespace SignupSystem.Models.DTO.Student
{
	public class GetStudentResponseDTO
	{
		public GetStudentResponseDTO()
		{
			RegisterClasses = new();
		}
		public ApplicationUser Student { get; set; }
		public List<RegisterClass> RegisterClasses { get; set; }
	}
}
