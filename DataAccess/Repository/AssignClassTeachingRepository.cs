using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class AssignClassTeachingRepository : Repository<AssignClassTeaching>, IAssignClassTeachingRepository
	{
		public AssignClassTeachingRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(AssignClassTeaching assignClassTeaching)
		{
			_db.Update(assignClassTeaching);
		}
	}
}
