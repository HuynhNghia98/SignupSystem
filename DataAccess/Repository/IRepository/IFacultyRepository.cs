using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IFacultyRepository : IRepository<Faculty>
	{
		void Update(Faculty Faculty);
	}
}
