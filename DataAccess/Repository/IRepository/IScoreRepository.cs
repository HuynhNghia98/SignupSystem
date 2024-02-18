using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IScoreRepository : IRepository<Score>
	{
		void Update(Score score);
	}
}
