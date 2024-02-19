namespace SignupSystem.Models.DTO.SubjectScoreType
{
	public class GetSubjectScoreTypeResponseDTO
	{
		public GetSubjectScoreTypeResponseDTO()
		{
			SubjectScoreTypes = new();
		}

		public List<Models.SubjectScoreType> SubjectScoreTypes { get; set; }
	}
}
