using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class ScoreTypeRepository : Repository<ScoreType>, IScoreTypeRepository
	{
		public ScoreTypeRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(ScoreType scoreType)
		{
			_db.Update(scoreType);
		}
	}
}
