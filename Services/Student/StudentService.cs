using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.DTO.ForgotPassword;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;
using SignupSystem.Services.Student.Interfaces;
using SignupSystem.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace SignupSystem.Services.Student
{
	public class StudentService : ControllerBase, IStudentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHost;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private ApiResponse<object> _res;
		public StudentService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHost = webHostEnvironment;
			_userManager = userManager;
			_db = db;
			_res = new();
		}

		public async Task<ApiResponse<GetStudentsResponseDTO>> GetStudentsAsync()
		{
			var students = await _unitOfWork.ApplicationUser.Get(x => x.IsStudent == true, true).ToListAsync();

			ApiResponse<GetStudentsResponseDTO> res = new();

			if (students == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result.Students = students;
			}
			return res;
		}

		public async Task<ApiResponse<GetStudentResponseDTO>> GetStudentAsync(string id)
		{
			var student = await _unitOfWork.ApplicationUser.Get(x => x.Id == id, true).FirstOrDefaultAsync();

			ApiResponse<GetStudentResponseDTO> res = new();

			if (student == null)
			{
				res.Errors = new Dictionary<string, List<string>>
				{
					{ "Error", new List<string> { "Không thể tìm thấy người dùng." } }
				};
				res.Result = null;
				res.IsSuccess = false;
			}
			else
			{
				var registerClasses = await _unitOfWork.RegisterClass.Get(x => x.ApplicationUserId == student.Id, true).Include(x=>x.Class).ToListAsync();

				if (registerClasses != null)
				{
					res.Result.RegisterClasses = registerClasses;
				}

				res.Result.Student = student;
			}
			return res;
		}

		public async Task<ApiResponse<GetStudentsResponseDTO>> SearchStudentsAsync(string search)
		{
			var student = await _unitOfWork.ApplicationUser
				.Get(x => x.UserCode.Contains(search) ||
				x.LastName.Contains(search) ||
				x.FirstName.Contains(search) ||
				x.Email.Contains(search) ||
				x.PhoneNumber.Contains(search)
				, true).ToListAsync();

			ApiResponse<GetStudentsResponseDTO> res = new();

			if (student == null || student.Count <= 0)
			{
				res.Errors = new Dictionary<string, List<string>>
				{
					{ "search", new List<string> { "Không thể tìm thấy người dùng." } }
				};
				res.Result = null;
				res.IsSuccess = false;
			}
			else
			{
				res.Result.Students = student;
			}
			return res;
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
					var StudentCount = await _db.ApplicationUsers.CountAsync(x => x.IsStudent == true);

					if (DateTime.TryParse(model.DOB, out DateTime dob))
					{
						var newStudent = new ApplicationUser()
						{
							UserCode = CreateUserCode.CreateCode(StudentCount),
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

							//add image
							if (model.File != null && model.File.Length > 0)
							{
								//root path
								string wwwRootPath = _webHost.WebRootPath;
								string userImagePath = Path.Combine(wwwRootPath, @"images");
								string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
								using (var fileStream = new FileStream(Path.Combine(userImagePath, fileName), FileMode.Create))
								{
									model.File.CopyTo(fileStream);
								}

								userInDb.ImageUrl = @"\images\" + fileName;
								_unitOfWork.ApplicationUser.Update(userInDb);
								_unitOfWork.Save();
							}
						}
						_res.Messages = "Đã thêm học viên thành công";
					}
					else
					{
						_res.IsSuccess = false;

						ModelStateHelper.AddModelError<AddStudentRequestDTO>(ModelState, nameof(AddStudentRequestDTO.DOB), "Ngày sinh không đúng.");
						_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
					}
				}

			}
			else
			{
				_res.IsSuccess = false;
			}
			return _res;
		}

		public async Task<ApiResponse<object>> UpdateStudentAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse<object>> DeleteStudentAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse<object>> GetStudentClassesAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse<object>> GetStudentSchedulesAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse<object>> PaySchoolFeeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
