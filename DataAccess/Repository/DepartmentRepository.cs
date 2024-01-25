using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class DepartmentRepository : Repository<Department>, IDepartmentRepository
	{
		public DepartmentRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Department Department)
		{
			_db.Update(Department);
		}
	}
}
