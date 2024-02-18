using SignupSystem.DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using SignupSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.DataAccess.Repository;
using SignupSystem.Services.Auth;
using SignupSystem.DataAccess.DbInitializer;
using SignupSystem.Services.Auth.Interfaces;
using SignupSystem.Utilities;
using SignupSystem.Services.Student.Interfaces;
using SignupSystem.Services.Student;
using SignupSystem.Services.Lecturer.Interfaces;
using SignupSystem.Services.Lecturer;
using Microsoft.OpenApi.Any;
using SignupSystem.Services.Class.Interfaces;
using SignupSystem.Services.Class;
using SignupSystem.Services.TrainningCourse.Interfaces;
using SignupSystem.Services.TrainningCourse;
using SignupSystem.Services.Subject.Interfaces;
using SignupSystem.Services.Subject;
using SignupSystem.Services.Department.Interfaces;
using SignupSystem.Services.Department;
using SignupSystem.Services.Faculty.Interfaces;
using SignupSystem.Services.Faculty;

namespace SignupSystem
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});

			//config swagger jwt token
			builder.Services.AddSwaggerGen(o =>
			{
				o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using Bearer scheme. \r\n\r\n" +
								  "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
								  "Example: \"Bearer 123456abcdef\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
				});
				o.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference=new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							},
							Scheme="oauth2",
							Name="Bearer",
							In=ParameterLocation.Header,
						},
						new List<string>()
					}
				});
				o.MapType<TimeSpan>(() => new OpenApiSchema
				{
					Type = "string",
					Example = new OpenApiString("00:00:00")
				});
			});

			//dbcontext
			builder.Services.AddDbContext<ApplicationDbContext>
				(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//identity dbcontext
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

			//config password
			builder.Services.Configure<IdentityOptions>(ops =>
			{
				ops.Password.RequireDigit = false;
				ops.Password.RequiredLength = 1;
				ops.Password.RequireUppercase = false;
				ops.Password.RequireLowercase = false;
				ops.Password.RequireNonAlphanumeric = false;
			});

			//truyền dữ liệu từ appsetting vào trong lớp MailSettings theo đúng tên các thuộc tính.
			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

			//config authentication
			var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
					ValidateIssuer = false,
					ValidateAudience = false,
				};
			});

			//builder.Services.AddCors();
			builder.Services.AddCors();

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IDbInitializer, DbInitializer>();
			builder.Services.AddScoped<IMailService, MailService>();
			builder.Services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
			builder.Services.AddScoped<IStudentService, StudentService>();
			builder.Services.AddScoped<ILecturerService, LecturerService>();
			builder.Services.AddScoped<IClassService, ClassService>();
			builder.Services.AddScoped<ISubjectService, SubjectService>();
			builder.Services.AddScoped<ITrainingCourseService, TrainingCourseService>();
			builder.Services.AddScoped<IDepartmentService, DepartmentService>();
			builder.Services.AddScoped<IFacultyService, FacultyService>();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			SeedData();

			app.Run();

			void SeedData()
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbInititalizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
					dbInititalizer.Initializer();
				}
			}
		}
	}
}