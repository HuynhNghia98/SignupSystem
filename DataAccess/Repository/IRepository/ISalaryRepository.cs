using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface ISalaryRepository : IRepository<Salary>
	{
		void Update(Salary salary);
	}
}
