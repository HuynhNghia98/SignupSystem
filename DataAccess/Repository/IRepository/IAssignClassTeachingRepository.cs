using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IAssignClassTeachingRepository : IRepository<AssignClassTeaching>
	{
		void Update(AssignClassTeaching assignClassTeaching);
	}
}
