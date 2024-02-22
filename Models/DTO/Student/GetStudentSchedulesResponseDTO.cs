namespace SignupSystem.Models.DTO.Student
{
	public class GetStudentSchedulesResponseDTO
	{
		public GetStudentSchedulesResponseDTO()
		{
			Classes = new();
		}
		public List<Models.Class> Classes { get; set; }
	}
}
