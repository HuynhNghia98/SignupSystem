using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class FeeTypeRepository : Repository<FeeType>, IFeeTypeRepository
	{
		public FeeTypeRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(FeeType FeeType)
		{
			_db.Update(FeeType);
		}
	}
}
