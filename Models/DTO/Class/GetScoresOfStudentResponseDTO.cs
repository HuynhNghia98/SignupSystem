namespace SignupSystem.Models.DTO.Class
{
	public class GetScoresOfStudentResponseDTO
	{
		public GetScoresOfStudentResponseDTO()
		{
			Scores = new();
		}
		public List<Score> Scores { get; set; }
	}
}
