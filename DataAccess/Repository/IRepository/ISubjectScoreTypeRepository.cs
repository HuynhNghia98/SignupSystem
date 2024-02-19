using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface ISubjectScoreTypeRepository : IRepository<SubjectScoreType>
	{
		void Update(SubjectScoreType subjectScoreType);
	}
}
