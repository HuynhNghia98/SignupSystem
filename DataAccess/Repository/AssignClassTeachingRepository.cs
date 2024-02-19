using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class AssignClassTeachingRepository : Repository<AssignClassTeach>, IAssignClassTeachRepository
	{
		public AssignClassTeachingRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(AssignClassTeach assignClassTeaching)
		{
			_db.Update(assignClassTeaching);
		}
	}
}
