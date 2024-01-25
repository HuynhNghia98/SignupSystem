using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class SubjectTeachRepository : Repository<SubjectTeach>, ISubjectTeachRepository
	{
		public SubjectTeachRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(SubjectTeach subjectTeach)
		{
			_db.Update(subjectTeach);
		}
	}
}
