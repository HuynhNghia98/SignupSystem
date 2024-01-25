using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IFeeTypeRepository : IRepository<FeeType>
	{
		void Update(FeeType FeeType);
	}
}
