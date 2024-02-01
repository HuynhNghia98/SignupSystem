
namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IApplicationUserRepository ApplicationUser { get; }
		IAssignClassTeachingRepository AssignClassTeaching { get; }
		IClassRepository Class { get; }
		IDepartmentRepository Department { get; }
		IFacultyRepository Faculty { get; }
		IFeeTypeRepository FeeType { get; }
		IRegisterClassRepository RegisterClass { get; }
		ISubjectRepository Subject { get; }
		ISubjectTeachRepository SubjectTeach { get; }

		void Save();
	}
}
