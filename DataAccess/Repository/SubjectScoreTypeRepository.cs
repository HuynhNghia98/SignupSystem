using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class SubjectScoreTypeRepository : Repository<SubjectScoreType>, ISubjectScoreTypeRepository
	{
		public SubjectScoreTypeRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(SubjectScoreType subjectScoreType)
		{
			_db.Update(subjectScoreType);
		}
	}
}
