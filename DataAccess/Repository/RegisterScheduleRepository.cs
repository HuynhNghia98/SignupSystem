using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class RegisterScheduleRepository : Repository<RegisterSchedule>, IRegisterScheduleRepository
	{
		public RegisterScheduleRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(RegisterSchedule registerSchedule)
		{
			_db.Update(registerSchedule);
		}
	}
}
