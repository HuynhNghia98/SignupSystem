using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IAssignClassTeachRepository : IRepository<AssignClassTeach>
	{
		void Update(AssignClassTeach assignClassTeaching);
	}
}
