using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class FacultyRepository : Repository<Faculty>, IFacultyRepository
	{
		public FacultyRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Faculty Faculty)
		{
			_db.Update(Faculty);
		}
	}
}
