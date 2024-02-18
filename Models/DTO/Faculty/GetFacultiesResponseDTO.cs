using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Faculty
{
	public class GetFacultiesResponseDTO
	{
		public GetFacultiesResponseDTO() 
		{
			Faculties = new();
		}

		public List<Models.Faculty> Faculties { get; set; }
	}
}
