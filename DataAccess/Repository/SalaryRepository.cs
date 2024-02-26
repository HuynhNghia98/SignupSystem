using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class SalaryRepository : Repository<Salary>, ISalaryRepository
	{
		public SalaryRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Salary salary)
		{
			_db.Update(salary);
		}
	}
}
