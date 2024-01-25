using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface ISubjectTeachRepository : IRepository<SubjectTeach>
	{
		void Update(SubjectTeach subjectTeach);
	}
}
