using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<RegisterClass> RegisterCourses { get; set; }
		public DbSet<AssignClassTeaching> AssignClassTeaching { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<FeeType> FeeTypes { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<RegisterSchedule> RegisterSchedules { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<SubjectTeach> SubjectTeaches { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            foreach (var e in modelBuilder.Model.GetEntityTypes())
            {
				var tableName = e.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					e.SetTableName(tableName.Substring(6));
				}
            }
		}

	}
}
