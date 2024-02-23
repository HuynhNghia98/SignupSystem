namespace SignupSystem.Models.DTO.Vacation
{
	public class GetVacationsResponseDTO
	{
		public GetVacationsResponseDTO()
		{
			Vacations = new();
		}
		public List<Models.Vacation> Vacations { get; set; }
	}
}
