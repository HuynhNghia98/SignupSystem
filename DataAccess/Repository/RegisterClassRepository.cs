using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class RegisterClassRepository : Repository<RegisterClass>, IRegisterClassRepository
	{
		public RegisterClassRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(RegisterClass registerClass)
		{
			_db.Update(registerClass);
		}
	}
}
