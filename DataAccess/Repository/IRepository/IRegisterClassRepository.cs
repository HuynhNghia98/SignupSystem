using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IRegisterClassRepository : IRepository<RegisterClass>
	{
		void Update(RegisterClass registerClass);
	}
}
