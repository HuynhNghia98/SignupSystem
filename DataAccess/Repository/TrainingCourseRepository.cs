using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class TrainingCourseRepository : Repository<TrainingCourse>, ITrainingCourseRepository
	{
		public TrainingCourseRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(TrainingCourse trainingCourse)
		{
			_db.Update(trainingCourse);
		}
	}
}
