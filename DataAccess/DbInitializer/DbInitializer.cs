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
					IsAdmin = true,
					DOB = new DateTime(1998, 12, 4)
				};

				_userManager.CreateAsync(newUser, "123").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(x => x.Email == newUser.Email);
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

				//Tạo class
				List<Class> newClassToAdd = new()
				{
					new Class()
					{
						Name = "Hoa",
					Fee = 1000000
					},
					new Class()
					{
						Name = "Hoa",
					Fee = 1000000
					},
				};
				foreach (var item in newClassToAdd)
				{
					_unitOfWork.Class.Add(item);

				}

				//Tạo thời khóa biều
				List<Schedule> newScheduleListToAdd = new()
				{
					new Schedule()
					{
						DayOfWeek = "Sáng Thứ 2",
						StartTime = new TimeSpan(8, 0, 0),
						EndTime = new TimeSpan(11, 30, 0),
					},
					new Schedule()
					{
						DayOfWeek = "Chiều Thứ 2",
						StartTime = new TimeSpan(13, 0, 0),
						EndTime = new TimeSpan(16, 30, 0),
					},
						new Schedule()
					{
						DayOfWeek = "Sáng Thứ 3",
						StartTime = new TimeSpan(8, 0, 0),
						EndTime = new TimeSpan(11, 30, 0),
					},
						new Schedule()
					{
						DayOfWeek = "Chiều Thứ 3",
						StartTime = new TimeSpan(13, 0, 0),
						EndTime = new TimeSpan(16, 30, 0),
					},
							new Schedule()
					{
						DayOfWeek = "Sáng Thứ 4",
						StartTime = new TimeSpan(8, 0, 0),
						EndTime = new TimeSpan(11, 30, 0),
					},
							new Schedule()
					{
						DayOfWeek = "Chiều Thứ 4",
						StartTime = new TimeSpan(13, 0, 0),
						EndTime = new TimeSpan(16, 30, 0),
					},
				};
                foreach (var item in newScheduleListToAdd)
                {
					_unitOfWork.Schedule.Add(item);
                }

                _unitOfWork.Save();

				//Đăng ký thời khóa biều cho lớp
				var newRegisterScheduleForCLass1 = new RegisterSchedule()
				{
					ClassId= 1,
					ScheduleId= 1,
				};
				_unitOfWork.RegisterSchedule.Add(newRegisterScheduleForCLass1);
				var newRegisterScheduleForCLass2 = new RegisterSchedule()
				{
					ClassId = 2,
					ScheduleId = 2,
				};
				_unitOfWork.RegisterSchedule.Add(newRegisterScheduleForCLass2);

				_unitOfWork.Save();
			}
			return;
		}
	}
}
