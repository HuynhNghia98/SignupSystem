using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Data;
using SignupSystem.Models;
using SignupSystem.Utilities;

namespace SignupSystem.DataAccess.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
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
					UserCode = "Admin001",
					UserName = "admin",
					Email = "nghiaht0412@gmail.com",
					FirstName = "Nghia",
					LastName = "Huynh",
					PhoneNumber = "0123456789",
					Address = "HCM",
					Gender = 0,
					IsAdmin = true,
					DOB = DateTime.Now
				};

				_userManager.CreateAsync(newUser, "123").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(x => x.Email == newUser.Email);
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
			}

			return;
		}
	}
}
