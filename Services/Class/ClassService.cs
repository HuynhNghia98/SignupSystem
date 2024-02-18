using Microsoft.AspNetCore.Mvc;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models.Response;
using SignupSystem.Services.Class.Interfaces;
using SignupSystem.Models.DTO.Class;
using Microsoft.EntityFrameworkCore;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Utilities;
using SignupSystem.Models;

namespace SignupSystem.Services.Class
{
	public class ClassService : ControllerBase, IClassService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHost;
		private ApiResponse<object> _res;
		public ClassService(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
		{
			_unitOfWork = unitOfWork;
			_res = new();
			_webHost = webHost;
		}

		public async Task<ApiResponse<GetClassesResponseDTO>> GetClassesAsync()
		{
			var classes = await _unitOfWork.Class.GetAll().Include(x => x.TrainingCourse)
														.Include(x => x.RegisterClasses)
														.ToListAsync();

			ApiResponse<GetClassesResponseDTO> res = new();
			res.Result.Classes = classes;

			return res;
		}
		public async Task<ApiResponse<Models.Class>> GetClassAsync(int id)
		{
			var classInDb = await _unitOfWork.Class.Get(x => x.Id == id, true).Include(x => x.TrainingCourse)
														.Include(x => x.RegisterClasses)
														.FirstOrDefaultAsync();

			ApiResponse<Models.Class> res = new();

			if (classInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = classInDb;
			}
			return res;
		}
		public async Task<ApiResponse<GetClassesResponseDTO>> SearchClassesAsync(string searchById, string searchByName)
		{
			ApiResponse<GetClassesResponseDTO> res = new();
			res.Result.Classes = await _unitOfWork.Class
				.Get(x => x.ClassCode.Contains(searchById) || x.Name.Contains(searchByName), true)
				.ToListAsync();
			return res;
		}
		public ApiResponse<object> AddClassAsync(AddOrUpdateClassRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.Class newClass = new()
				{
					TrainingCourseId = model.TrainingCourseId,
					FacultyId = model.FacultyId,
					ClassCode = model.ClassCode,
					Name = model.ClassName,
					Fee = model.Fee,
					StudentQuantity = model.StudentQuantity,
					Detail = model.Detail,
					Status = model.Status,
			};

				//add image
				if (model.File != null && model.File.Length > 0)
				{
					//root path
					string wwwRootPath = _webHost.WebRootPath;
					string classImagePath = Path.Combine(wwwRootPath, @"images/class");
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
					using (var fileStream = new FileStream(Path.Combine(classImagePath, fileName), FileMode.Create))
					{
						model.File.CopyTo(fileStream);
					}

					newClass.ImageUrl = @"\images\class\" + fileName;
				}

				_unitOfWork.Class.Add(newClass);
				_unitOfWork.Save();

				_res.Messages = "Thêm lớp học thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> UpdateClassAsync(int classId, AddOrUpdateClassRequestDTO model)
		{
			if (classId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (ModelState.IsValid)
			{
				var classInDb = await _unitOfWork.Class.Get(x => x.Id == classId, true).FirstOrDefaultAsync();

				if (classInDb == null)
				{
					_res.IsSuccess = false;
					return _res;
				}

				classInDb.TrainingCourseId = model.TrainingCourseId;
				classInDb.FacultyId = model.FacultyId;
				classInDb.ClassCode = model.ClassCode;
				classInDb.Name = model.ClassName;
				classInDb.Fee = model.Fee;
				classInDb.StudentQuantity = model.StudentQuantity;
				classInDb.Detail = model.Detail;
				classInDb.Status = model.Status;

				//update image
				if (model.File != null && model.File.Length > 0)
				{
					//root path
					string wwwRootPath = _webHost.WebRootPath;
					string classImagePath = Path.Combine(wwwRootPath, @"images/class");

					//remove image
					if (!string.IsNullOrEmpty(classInDb.ImageUrl))
					{
						string imagePath = Path.Combine(wwwRootPath, classInDb.ImageUrl.TrimStart('\\'));

						if (System.IO.File.Exists(imagePath))
						{
							System.IO.File.Delete(imagePath);
						}
					}

					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
					using (var fileStream = new FileStream(Path.Combine(classImagePath, fileName), FileMode.Create))
					{
						model.File.CopyTo(fileStream);
					}

					classInDb.ImageUrl = @"\images\class\" + fileName;
				}

				_unitOfWork.Class.Update(classInDb);
				_unitOfWork.Save();

				_res.Messages = "Đã cập nhật lớp thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> DeleteClassAsync(int classId)
		{
			if (classId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var classToDelete = await _unitOfWork.Class.Get(x => x.Id == classId, true).FirstOrDefaultAsync();

			if (classToDelete == null)
			{
				_res.IsSuccess = false;
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { "Không thể tìm thấy lớp." } }
					};
				return _res;
			}

			if (classToDelete.ImageUrl != null)
			{
				//root path
				string wwwRootPath = _webHost.WebRootPath;
				string userImagePath = Path.Combine(wwwRootPath, @"images/class");

				//Xóa ảnh lớp
				if (!string.IsNullOrEmpty(classToDelete.ImageUrl))
				{
					string imagePath = Path.Combine(wwwRootPath, classToDelete.ImageUrl.TrimStart('\\'));

					if (System.IO.File.Exists(imagePath))
					{
						System.IO.File.Delete(imagePath);
					}
				}
			}

			_unitOfWork.Class.Remove(classToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa lớp thành công";
			return _res;
		}

		public async Task<ApiResponse<GetSubjectsResponseDTO>> GetSubjectListOfClassAsync(int classId)
		{
			var assignClassTeachingList = await _unitOfWork.AssignClassTeaching
				.Get(x => x.ClassId == classId, true)
				.Include(x => x.Subject)
				.ToListAsync();

			var subjectList = assignClassTeachingList.Select(x => x.Subject).ToList();

			ApiResponse<GetSubjectsResponseDTO> res = new();
			res.Result.Subjects = subjectList;
			return res;
		}

		public async Task<ApiResponse<GetStudentsResponseDTO>> GetStudentListOfClassAsync(int classId)
		{
			var registerClassList = await _unitOfWork.RegisterClass
				.Get(x => x.ClassId == classId && x.PaymentStatus == SD.Payment_Paid, true)
				.Include(x => x.ApplicationUser)
				.ToListAsync();

			var studentList = registerClassList.Select(x => x.ApplicationUser).ToList();

			ApiResponse<GetStudentsResponseDTO> res = new();
			res.Result.Students = studentList;
			return res;
		}

		public ApiResponse<object> AddScoreForClassAsync(int classId, string studentId)
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse<GetStudentsResponseDTO>> GetScoreOfClassAsync(int classId)
		{
			throw new NotImplementedException();
		}
	}
}