
namespace SignupSystem.Models.DTO.TrainingCourse
{
    public class GetTrainingCoursesResponseDTO
    {
        public GetTrainingCoursesResponseDTO()
        {
			TrainingCourses = new();
        }
        public List<Models.TrainingCourse> TrainingCourses { get; set; }
    }
}
