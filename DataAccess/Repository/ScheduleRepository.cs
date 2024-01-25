using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
	{
		public ScheduleRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Schedule schedule)
		{
			_db.Update(schedule);
		}
	}
}
