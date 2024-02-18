namespace SignupSystem.Models.DTO.Subject
{
    public class GetSubjectsResponseDTO
    {
        public GetSubjectsResponseDTO()
        {
			Subjects = new();
        }
        public List<Models.Subject> Subjects { get; set; }
    }
}
