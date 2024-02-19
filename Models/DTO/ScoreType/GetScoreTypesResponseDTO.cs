namespace SignupSystem.Models.DTO.ScoreType
{
	public class GetScoreTypesResponseDTO
	{
		public GetScoreTypesResponseDTO()
		{
			ScoreTypes = new();
		}

		public List<Models.ScoreType> ScoreTypes { get; set; }
	}
}
