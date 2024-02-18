namespace SignupSystem.Models.DTO.Class
{
    public class GetClassesResponseDTO
    {
        public GetClassesResponseDTO()
        {
            Classes = new();
        }
        public List<Models.Class> Classes { get; set; }
    }
}
