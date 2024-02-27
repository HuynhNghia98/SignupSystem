using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Utilities;

namespace SignupSystem.DataAccess.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
			_unitOfWork = unitOfWork;
		}

		public void Initializer()
		{
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex) { }

			//Tạo Role nếu không có
			if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Accountant)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_RegistrationDepartment)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_BoardOfManager)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Lecturer)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Student)).GetAwaiter().GetResult();

				//Tạo tài khoản admin
				var newUser = new ApplicationUser
				{
					UserCode = "Admin-001",
					UserName = "admin",
					Email = "nghiaht0412@gmail.com",
					FirstName = "Nghia",
					LastName = "Huynh",
					PhoneNumber = "0123456789",
					Address = "HCM",
					Gender = 0,
					IsEmployee = true,
					DOB = new DateTime(1998, 12, 4)
				};

				_userManager.CreateAsync(newUser, "123").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(x => x.Email == newUser.Email);
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

				//Tạo Khóa đào tạo
				TrainingCourse trainingCourse = new()
				{
					TrainingCourseCode = "DT01",
					Name = "khoa dao tao 1",
				};
				_unitOfWork.TrainingCourse.Add(trainingCourse);
				//Tạo Khoa
				Faculty faculty = new()
				{
					FacultyCode = "K01",
					Name = "khoa IT",
				};
				_unitOfWork.Faculty.Add(faculty);
				_unitOfWork.Save();


				//Tạo class
				List<Class> newClassToAdd = new()
				{
					new Class()
					{
						ClassCode="L01",
						Name = "Họa - 001",
						Fee = 1000000,
						OpenStatus = true,
						TrainingCourseId=1,
						FacultyId=1,

					},
					new Class()
					{
						ClassCode="L02",
						Name = "Hát - 001",
						Fee = 1000000,
						OpenStatus = true,
						TrainingCourseId=1,
						FacultyId=1,
					},
				};
				foreach (var item in newClassToAdd)
				{
					_unitOfWork.Class.Add(item);
				}

				//Tạo bộ môn
				Department department = new()
				{
					Name = "Bộ môn Mỹ Thuật",
					Details = "Dạy các môn về mỹ thuật"
				};
				_unitOfWork.Department.Add(department);
				_unitOfWork.Save();
				// Tạo subject
				Subject subject = new()
				{
					Name = "Mỹ Thuật - 001",
					Details = "Dạy các môn về mỹ thuật",
					FacultyId = faculty.Id,
					DepartmentId = department.Id
				};
				_unitOfWork.Subject.Add(subject);
				_unitOfWork.Save();

				var newFeeType = new FeeType()
				{
					FeeTypeName = "Thu Toàn bộ khóa học (100%)",
					FeeTypeDetails = "Thu Toàn bộ khóa học (100%)",
				};
				_unitOfWork.FeeType.Add(newFeeType);
				_unitOfWork.Save();
			}
			return;
		}
	}
}
