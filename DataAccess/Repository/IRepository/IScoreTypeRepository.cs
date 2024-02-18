using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IScoreTypeRepository : IRepository<ScoreType>
	{
		void Update(ScoreType scoreType);
	}
}
