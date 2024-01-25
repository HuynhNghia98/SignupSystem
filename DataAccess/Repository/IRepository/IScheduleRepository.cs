using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IScheduleRepository : IRepository<Schedule>
	{
		void Update(Schedule schedule);
	}
}
