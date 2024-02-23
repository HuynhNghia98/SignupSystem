using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class VacationRepository : Repository<Vacation>, IVacationRepository
	{
		public VacationRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Vacation vacation)
		{
			_db.Update(vacation);
		}
	}
}
