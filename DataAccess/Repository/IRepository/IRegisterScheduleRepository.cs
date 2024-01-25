using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IRegisterScheduleRepository : IRepository<RegisterSchedule>
	{
		void Update(RegisterSchedule registerSchedule);
	}
}
