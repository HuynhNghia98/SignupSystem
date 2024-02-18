namespace SignupSystem.Models.DTO.Department
{
    public class GetDepartmentsResponseDTO
	{
        public GetDepartmentsResponseDTO()
        {
			Departments = new();
        }
        public List<Models.Department> Departments { get; set; }
    }
}
