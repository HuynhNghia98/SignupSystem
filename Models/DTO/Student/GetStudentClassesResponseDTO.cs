using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Student
{
	public class GetStudentClassesResponseDTO
	{
		public GetStudentClassesResponseDTO()
		{
			RegisterClasses = new();
		}
		public List<RegisterClass> RegisterClasses { get; set; }
	}
}
