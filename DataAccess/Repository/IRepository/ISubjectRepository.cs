using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface ISubjectRepository : IRepository<Subject>
	{
		void Update(Subject subject);
	}
}
