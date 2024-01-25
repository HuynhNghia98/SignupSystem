using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IClassRepository : IRepository<Class>
	{
		void Update(Class Class);
	}
}
