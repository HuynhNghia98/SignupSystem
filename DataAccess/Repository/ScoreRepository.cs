using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class ScoreRepository : Repository<Score>, IScoreRepository
	{
		public ScoreRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Score score)
		{
			_db.Update(score);
		}
	}
}
