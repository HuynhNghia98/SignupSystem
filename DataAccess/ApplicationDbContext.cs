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
		public DbSet<AssignClassTeach> AssignClassTeaches { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<FeeType> FeeTypes { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<SubjectTeach> SubjectTeaches { get; set; }
		public DbSet<TrainingCourse> TrainingCourses { get; set; }
		public DbSet<Score> Scores { get; set; }
		public DbSet<ScoreType> ScoreTypes { get; set; }
		public DbSet<Vacation> Vacations { get; set; }
		public DbSet<Salary> Salaries { get; set; }

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

				var subject = modelBuilder.Entity<Subject>();
				subject.HasMany(s => s.AssignClassTeaches)
			   .WithOne(m => m.Subject)
			   .HasForeignKey(m => m.SubjectId)
			   .OnDelete(DeleteBehavior.NoAction); // sửa lỗi Introducing FOREIGN KEY constraint

				subject.HasMany(s => s.Scores)
			   .WithOne(m => m.Subject)
			   .HasForeignKey(m => m.SubjectId)
			   .OnDelete(DeleteBehavior.NoAction); // sửa lỗi Introducing FOREIGN KEY constraint

				var assignClassTeach = modelBuilder.Entity<AssignClassTeach>();
				assignClassTeach.HasOne(m => m.ApplicationUser)
				.WithMany(s => s.AssignClassTeaches);
				assignClassTeach.HasOne(m => m.Class)
				.WithMany(t => t.AssignClassTeaches);
				assignClassTeach.HasOne(m => m.Subject)
				.WithMany(t => t.AssignClassTeaches);
			}

		}

	}
}
