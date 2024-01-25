using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.ForgotPassword;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;
using SignupSystem.Services.Student.Interfaces;
using SignupSystem.Utilities;

namespace SignupSystem.Services.Student
{
	public class StudentService : ControllerBase, IStudentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private ApiResponse<object> _res;
		public StudentService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_db = db;
			_res = new();
		}

		public Task<ApiResponse<object>> GetStudentsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ApiResponse<object>> GetStudentAsync()
		{
			throw new NotImplementedException();
		}
		public Task<ApiResponse<object>> SearchStudentsAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse<object>> AddStudentAsync(AddStudentRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = await _unitOfWork.ApplicationUser.Get(x => x.Email == model.Email, true).FirstOrDefaultAsync();

				if (user != null)
				{
					_res.IsSuccess = false;

					ModelStateHelper.AddModelError<AddStudentRequestDTO>(ModelState, nameof(AddStudentRequestDTO.Email), "Email đã tồn tại.");
					_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
				}
				else
				{
					if (DateTime.TryParse(model.DOB, out DateTime dob))
					{
						var newStudent = new ApplicationUser()
						{
							IsStudent = true,
							LastName = model.LastName,
							FirstName = model.FirstName,
							DOB = dob,
							Gender = model.Gender,
							Email = model.Email,
							UserName = model.Email,
							PhoneNumber = model.PhoneNumer,
							Address = model.Address,
							Parents = model.Parents,
						};

						var result = await _userManager.CreateAsync(newStudent, model.Password);

						if (result.Succeeded)
						{
							var userInDb = _db.ApplicationUsers.FirstOrDefault(x => x.Id == newStudent.Id);

							if (userInDb == null)
							{
								_res.IsSuccess = false;
								return _res;
							}
							//thêm role
							_userManager.AddToRoleAsync(userInDb, SD.Role_Student).GetAwaiter().GetResult();
							//đăng ký lớp
							var registerClass = new RegisterClass()
							{
								ApplicationUserId = userInDb.Id,
								ClassId = model.ClassId
							};

							_unitOfWork.RegisterClass.Add(registerClass);
							_unitOfWork.Save();

							_res.Messages = "Đã thêm học viên thành công";
							return _res;
						}
						_res.IsSuccess = false;
						return _res;
					}
					_res.IsSuccess = false;

					ModelStateHelper.AddModelError<AddStudentRequestDTO>(ModelState, nameof(AddStudentRequestDTO.DOB), "Ngày sinh không đúng.");
					_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
				}

				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}

		public Task<ApiResponse<object>> UpdateStudentAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ApiResponse<object>> DeleteStudentAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ApiResponse<object>> GetStudentClassesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ApiResponse<object>> GetStudentSchedulesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ApiResponse<object>> PaySchoolFeeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
