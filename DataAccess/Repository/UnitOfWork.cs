using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;

namespace SignupSystem.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDbContext _db;
		public IApplicationUserRepository ApplicationUser { get; private set; }


		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			ApplicationUser = new ApplicationUserRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

	}
}
