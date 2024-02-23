using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IVacationRepository : IRepository<Vacation>
	{
		void Update(Vacation vacation);
	}
}
