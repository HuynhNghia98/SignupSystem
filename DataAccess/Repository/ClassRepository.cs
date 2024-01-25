using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class ClassRepository : Repository<Class>, IClassRepository
	{
		public ClassRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Class Class)
		{
			_db.Update(Class);
		}
	}
}
