namespace SignupSystem.Models.DTO.Class
{
	public class GetScoreOfClassResponseDTO
	{
		public GetScoreOfClassResponseDTO()
		{
			Scores = new();
			SubjectScoreTypes = new();
		}
		public List<Score> Scores { get; set; }
		public List<Models.SubjectScoreType> SubjectScoreTypes { get; set; }
	}
}
