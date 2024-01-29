namespace SignupSystem.Models.DTO.Student
{
	public class GetClassesResponseDTO
	{
		public GetClassesResponseDTO()
		{
			Classes = new();
		}
		public List<Class> Classes { get; set; }
	}
}
