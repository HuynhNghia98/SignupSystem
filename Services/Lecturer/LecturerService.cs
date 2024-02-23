using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Class;
using SignupSystem.Models.DTO.Lecturer;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.Response;
using SignupSystem.Services.Lecturer.Interfaces;
using SignupSystem.Utilities;

namespace SignupSystem.Services.Lecturer
{
	public class LecturerService : ControllerBase, ILecturerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHost;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private ApiResponse<object> _res;
		public LecturerService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHost = webHostEnvironment;
			_userManager = userManager;
			_db = db;
			_res = new();
		}
		public async Task<ApiResponse<GetLecturersResponseDTO>> GetLecturersAsync()
		{
			var lecturers = await _unitOfWork.ApplicationUser.Get(x => x.IsLecturer == true, true).ToListAsync();

			ApiResponse<GetLecturersResponseDTO> res = new();

			if (lecturers == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result.Lecturers = lecturers;
			}
			return res;
		}
		public async Task<ApiResponse<GetLecturerResponseDTO>> GetLecturerAsync(string id)
		{
			var lecturer = await _unitOfWork.ApplicationUser.Get(x => x.Id == id, true).FirstOrDefaultAsync();

			ApiResponse<GetLecturerResponseDTO> res = new();

			if (lecturer == null)
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
				res.Result.Lecturer = lecturer;
			}
			return res;
		}
		public async Task<ApiResponse<GetLecturersResponseDTO>> SearchLecturersAsync(string search)
		{
			var lecturers = await _unitOfWork.ApplicationUser
				.Get(x => x.IsLecturer == true && (x.UserCode.Contains(search) ||
				x.LastName.Contains(search) ||
				x.FirstName.Contains(search) ||
				x.Email.Contains(search) ||
				x.PhoneNumber.Contains(search))
				, true).ToListAsync();

			ApiResponse<GetLecturersResponseDTO> res = new();

			if (lecturers == null || lecturers.Count <= 0)
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
				res.Result.Lecturers = lecturers;
			}
			return res;
		}
		public async Task<ApiResponse<object>> AddLecturerAsync(AddLecturerRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = await _unitOfWork.ApplicationUser.Get(x => x.Email == model.Email, true).FirstOrDefaultAsync();

				if (user != null)
				{
					_res.IsSuccess = false;

					ModelStateHelper.AddModelError<AddLecturerRequestDTO>(ModelState, nameof(AddLecturerRequestDTO.Email), "Email đã tồn tại.");
					_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
				}
				else
				{
					if (DateTime.TryParse(model.DOB, out DateTime dob))
					{
						var newLecturer = new ApplicationUser()
						{
							UserCode = model.Id,
							TaxCode = model.TaxCode,
							IsLecturer = true,
							LastName = model.LastName,
							FirstName = model.FirstName,
							DOB = dob,
							Gender = model.Gender,
							Email = model.Email,
							UserName = model.Email,
							PhoneNumber = model.PhoneNumber,
							Address = model.Address,
						};

						var result = await _userManager.CreateAsync(newLecturer, model.Password);

						if (result.Succeeded)
						{
							var userInDb = _db.ApplicationUsers.FirstOrDefault(x => x.Id == newLecturer.Id);

							if (userInDb == null)
							{
								_res.IsSuccess = false;
								return _res;
							}
							// Thêm role
							_userManager.AddToRoleAsync(userInDb, SD.Role_Lecturer).GetAwaiter().GetResult();

							var classinfor = await _unitOfWork.Class.Get(x => x.Id == model.SubjectId, true).FirstOrDefaultAsync();

							// Dậy môn
							var SubjectTeach = new SubjectTeach()
							{
								ApplicationUserId = userInDb.Id,
								// Dậy môn chính
								SubjectId = model.SubjectId,
								// Dậy môn kiêm nhiệm
								SecondSubject = model.SecondSubject,
							};
							_unitOfWork.SubjectTeach.Add(SubjectTeach);
							_unitOfWork.Save();

							// Thêm hình ảnh
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
						_res.Messages = "Đã thêm giảng viên thành công";
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
		public async Task<ApiResponse<object>> UpdateLecturerAsync(string userId, UpdateLecturerRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = await _unitOfWork.ApplicationUser.Get(x => x.Id == userId, true).FirstOrDefaultAsync();

				if (user == null)
				{
					_res.IsSuccess = false;

					ModelStateHelper.AddModelError<UpdateLecturerRequestDTO>(ModelState, nameof(UpdateLecturerRequestDTO.Email), "Người dùng không tồn tại.");
					_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
				}
				else
				{
					var userInDbSameEmail = await _unitOfWork.ApplicationUser.Get(x => x.Email == model.Email, true).FirstOrDefaultAsync();

					if (userInDbSameEmail != null && userInDbSameEmail.Id != user.Id)
					{
						_res.IsSuccess = false;

						ModelStateHelper.AddModelError<UpdateLecturerRequestDTO>(ModelState, nameof(UpdateLecturerRequestDTO.DOB), "Email đã tồn tại.");
						_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
					}
					else
					{
						if (DateTime.TryParse(model.DOB, out DateTime dob))
						{
							user.TaxCode = model.TaxCode;
							user.LastName = model.LastName;
							user.FirstName = model.FirstName;
							user.DOB = dob;
							user.Gender = model.Gender;
							user.Email = model.Email;
							user.UserName = model.Email;
							user.PhoneNumber = model.PhoneNumber;

							//update image
							if (model.File != null && model.File.Length > 0)
							{
								//root path
								string wwwRootPath = _webHost.WebRootPath;
								string userImagePath = Path.Combine(wwwRootPath, @"images");

								//remove image
								if (!string.IsNullOrEmpty(user.ImageUrl))
								{
									string imagePath = Path.Combine(wwwRootPath, user.ImageUrl.TrimStart('\\'));

									if (System.IO.File.Exists(imagePath))
									{
										System.IO.File.Delete(imagePath);
									}
								}

								string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
								using (var fileStream = new FileStream(Path.Combine(userImagePath, fileName), FileMode.Create))
								{
									model.File.CopyTo(fileStream);
								}

								user.ImageUrl = @"\images\" + fileName;
							}
							_unitOfWork.ApplicationUser.Update(user);
							_unitOfWork.Save();
							_res.Messages = "Cập nhật giảng viên thành công.";
						}
						else
						{
							_res.IsSuccess = false;

							ModelStateHelper.AddModelError<UpdateStudentRequestDTO>(ModelState, nameof(UpdateStudentRequestDTO.DOB), "Ngày sinh không đúng.");
							_res.Errors = ModelStateHelper.ConvertToDictionary(ModelState);
						}
					}
				}
			}
			else
			{
				_res.IsSuccess = false;
			}
			return _res;
		}
		public async Task<ApiResponse<object>> DeleteLecturerAsync(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				_res.IsSuccess = false;
			}
			else
			{
				var user = await _unitOfWork.ApplicationUser.Get(x => x.Id == userId && x.IsLecturer == true, true).FirstOrDefaultAsync();

				if (user == null)
				{
					_res.IsSuccess = false;
					_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { "Không thể tìm thấy giảng viên." } }
					};
				}
				else
				{
					//root path
					string wwwRootPath = _webHost.WebRootPath;
					string userImagePath = Path.Combine(wwwRootPath, @"images");

					//Xóa ảnh giảng viên
					if (!string.IsNullOrEmpty(user.ImageUrl))
					{
						string imagePath = Path.Combine(wwwRootPath, user.ImageUrl.TrimStart('\\'));

						if (System.IO.File.Exists(imagePath))
						{
							System.IO.File.Delete(imagePath);
						}
					}

					_unitOfWork.ApplicationUser.Remove(user);
					_unitOfWork.Save();

					_res.Messages = "Đã xóa giảng viên thành công.";
				}
			}
			return _res;
		}
		public async Task<ApiResponse<object>> GetAndSearchTeachingAssignmentAsync(string? search, int classId)
		{
			var teachingAssignment = new List<AssignClassTeach>();

			//lấy theo search
			if (string.IsNullOrEmpty(search))
			{
				//lấy tất cả phân công giảng dạy
				teachingAssignment = await _unitOfWork.AssignClassTeach.GetAll().ToListAsync();
			}
			else
			{
				//lấy các phân công giảng dạy theo tìm kiếm và có IsLecturer == true
				teachingAssignment = await _unitOfWork.AssignClassTeach
				   .Get(x => x.ApplicationUser.IsLecturer == true && (
				   x.ApplicationUser.UserCode.Contains(search) ||
				   x.ApplicationUser.FirstName.Contains(search) ||
				   x.ApplicationUser.LastName.Contains(search) ||
				   x.Subject.Name.Contains(search)), true)
				   .ToListAsync();
			}

			//lọc theo lớp
			if (classId == 0)
			{
				//tìm kiếm tất cả phân công giảng dạy
				_res.Result = teachingAssignment;
			}
			else
			{
				//tìm kiếm tất cả phân công giảng dạy theo lớp
				_res.Result = teachingAssignment.Where(x => x.ClassId == classId);

			}
			return _res;
		}
		public async Task<ApiResponse<object>> AddTeachingAssignmentAsync(AddTeachingAssignmentRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				if (model.ClassId == 0 || model.SubjectId == 0)
				{
					_res.IsSuccess = false;
					return _res;
				}

				if (model.DayOfWeek < 2 || model.DayOfWeek > 8)
				{
					_res.IsSuccess = false;
					_res.Errors = new Dictionary<string, List<string>>
					{
						{ "dayOfWeek", new List<string> { "Chọn thứ từ thứ 2 đến chủ nhật (2-8)." } }
					};

					return _res;
				}

				//kiểm tra lớp có chưa
				var classinfor = await _unitOfWork.Class.Get(x => x.Id == model.ClassId, true).FirstOrDefaultAsync();

				if (classinfor == null)
				{
					_res.IsSuccess = false;
					_res.Errors = new Dictionary<string, List<string>>
					{
						{ "classId", new List<string> { "Không thể tìm thấy lớp." } }
					};

					return _res;
				}

				//Kiểm tra trùng
				var assignClassTeachingInDb = await _unitOfWork.AssignClassTeach
					.Get(x => x.ApplicationUserId == model.LecturerId &&
								x.ClassId == model.ClassId &&
								x.SubjectId == model.SubjectId &&
								x.DayOfWeek == model.DayOfWeek &&
								x.StartTime == model.StartTime , true)
					.FirstOrDefaultAsync();

				if (assignClassTeachingInDb != null)
				{
					_res.IsSuccess = false;
					_res.Errors = new Dictionary<string, List<string>>
					{
						{ "classId", new List<string> { "Đã phân công giảng dạy lớp này." } }
					};

					return _res;
				}

				//kiểm tra lớp có mở chưa
				if (classinfor.OpenStatus == false)
				{
					_res.IsSuccess = false;
					_res.Errors = new Dictionary<string, List<string>>
					{
						{ "classId", new List<string> { "Lớp chưa mở." } }
					};

					return _res;
				}

				AssignClassTeach newwAssignClassTeach = new()
				{
					DayOfWeek = model.DayOfWeek,
					StartTime = model.StartTime,
					EndTime = model.EndTime,
					StartDay = model.StartDay,
					EndDay = model.EndDay,
					ClassId = model.ClassId,
					SubjectId = model.SubjectId,
					ApplicationUserId = model.LecturerId,
					Details = model.Detail,
				};

				_unitOfWork.AssignClassTeach.Add(newwAssignClassTeach);
				_unitOfWork.Save();

				_res.Messages = "Đã thêm lịch giảng dạy thành công.";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
	}
}
