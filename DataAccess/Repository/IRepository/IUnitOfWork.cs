
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
		IPaymentRepository Payment { get; }
		IRegisterClassRepository RegisterClass { get; }
		IRegisterScheduleRepository RegisterSchedule { get; }
		IScheduleRepository Schedule { get; }
		ISubjectRepository Subject { get; }
		ISubjectTeachRepository SubjectTeach { get; }

		void Save();
	}
}
