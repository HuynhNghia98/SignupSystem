using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDbContext _db;
		public IApplicationUserRepository ApplicationUser { get; private set; }
		public IAssignClassTeachRepository AssignClassTeach { get; private set; }
		public IClassRepository Class { get; private set; }
		public IDepartmentRepository Department { get; private set; }
		public IFacultyRepository Faculty { get; private set; }
		public IFeeTypeRepository FeeType { get; private set; }
		public IRegisterClassRepository RegisterClass { get; private set; }
		public ISubjectRepository Subject { get; private set; }
		public ISubjectTeachRepository SubjectTeach { get; private set; }
		public ISubjectScoreTypeRepository SubjectScoreType { get; private set; }
		public IScoreRepository Score { get; private set; }
		public IScoreTypeRepository ScoreType { get; private set; }
		public ITrainingCourseRepository TrainingCourse { get; private set; }

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			ApplicationUser = new ApplicationUserRepository(_db);
			AssignClassTeach = new AssignClassTeachingRepository(_db);
			Class = new ClassRepository(_db);
			Department = new DepartmentRepository(_db);
			Faculty = new FacultyRepository(_db);
			FeeType = new FeeTypeRepository(_db);
			RegisterClass = new RegisterClassRepository(_db);
			Subject = new SubjectRepository(_db);
			SubjectTeach = new SubjectTeachRepository(_db);
			SubjectScoreType = new SubjectScoreTypeRepository(_db);
			Score = new ScoreRepository(_db);
			ScoreType = new ScoreTypeRepository(_db);
			TrainingCourse = new TrainingCourseRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

	}
}
