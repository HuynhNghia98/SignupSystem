using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class SubjectRepository : Repository<Subject>, ISubjectRepository
	{
		public SubjectRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Subject subject)
		{
			_db.Update(subject);
		}
	}
}
