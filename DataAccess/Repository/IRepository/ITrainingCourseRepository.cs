using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface ITrainingCourseRepository : IRepository<TrainingCourse>
	{
		void Update(TrainingCourse trainingCourse);
	}
}
