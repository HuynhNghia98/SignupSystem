using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDbContext _db;
		public IApplicationUserRepository ApplicationUser { get; private set; }
		public IAssignClassTeachingRepository AssignClassTeaching { get; private set; }
		public IClassRepository Class { get; private set; }
		public IDepartmentRepository Department { get; private set; }
		public IFacultyRepository Faculty { get; private set; }
		public IFeeTypeRepository FeeType { get; private set; }
		public IPaymentRepository Payment { get; private set; }
		public IRegisterClassRepository RegisterClass { get; private set; }
		public IRegisterScheduleRepository RegisterSchedule { get; private set; }
		public IScheduleRepository Schedule { get; private set; }
		public ISubjectRepository Subject { get; private set; }
		public ISubjectTeachRepository SubjectTeach { get; private set; }

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			ApplicationUser = new ApplicationUserRepository(_db);
			AssignClassTeaching = new AssignClassTeachingRepository(_db);
			Class = new ClassRepository(_db);
			Department = new DepartmentRepository(_db);
			Faculty = new FacultyRepository(_db);
			FeeType = new FeeTypeRepository(_db);
			Payment = new PaymentRepository(_db);
			RegisterClass = new RegisterClassRepository(_db);
			RegisterSchedule = new RegisterScheduleRepository(_db);
			Schedule = new ScheduleRepository(_db);
			Subject = new SubjectRepository(_db);
			SubjectTeach = new SubjectTeachRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

	}
}
