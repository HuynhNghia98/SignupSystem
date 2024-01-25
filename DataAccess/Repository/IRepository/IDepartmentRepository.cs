using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IDepartmentRepository : IRepository<Department>
	{
		void Update(Department Department);
	}
}
