
namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IApplicationUserRepository ApplicationUser { get; }
		IAssignClassTeachRepository AssignClassTeach { get; }
		IClassRepository Class { get; }
		IDepartmentRepository Department { get; }
		IFacultyRepository Faculty { get; }
		IFeeTypeRepository FeeType { get; }
		IRegisterClassRepository RegisterClass { get; }
		ISubjectRepository Subject { get; }
		ISubjectTeachRepository SubjectTeach { get; }
		ISubjectScoreTypeRepository SubjectScoreType { get; }
		IScoreRepository Score { get; }
		IScoreTypeRepository ScoreType { get; }
		ITrainingCourseRepository TrainingCourse { get; }
		IVacationRepository Vacation { get; }
		void Save();
		void SaveAsync();
	}
}
