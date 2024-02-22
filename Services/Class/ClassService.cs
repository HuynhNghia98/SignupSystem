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
using SignupSystem.DataAccess.Data;

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

		//new 

		public async Task<ApiResponse<GetSubjectsResponseDTO>> GetSubjectListOfClassAsync(int classId)
		{
			var assignClassTeachingList = await _unitOfWork.AssignClassTeach
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
				.Get(x => x.ClassId == classId, true)
				.Include(x => x.ApplicationUser)
				.ToListAsync();

			var studentList = registerClassList.Select(x => x.ApplicationUser).ToList();

			ApiResponse<GetStudentsResponseDTO> res = new();
			res.Result.Students = studentList;
			return res;
		}

		public async Task<ApiResponse<object>> AddScoreForStudentAsync(AddScoreForStudentRequestDTO model)
		{
			if (model.ScoreTypeId == 0 || model.ClassId == 0 || model.SubjectId == 0 || string.IsNullOrEmpty(model.StudentId))
			{
				_res.IsSuccess = false;
				return _res;
			}

			//lấy thông tin lớp
			var classInfor = await _unitOfWork.Class.Get(x => x.Id == model.ClassId, true).FirstOrDefaultAsync();

			if (classInfor == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (classInfor.FinalizeStudentScores)
			{
				//báo lỗi đã chốt điểm
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Đã chốt điểm" } }
					};
				_res.IsSuccess = false;
				return _res;
			}

			//lấy mã khóa đào tạo
			var trainingCourseId = classInfor.TrainingCourseId;

			//lấy loại điểm môn để biết số cột điẻm
			var subjectScoreType = await _unitOfWork.SubjectScoreType
				.Get(x => x.SubjectId == model.SubjectId && x.TrainingCourseId == trainingCourseId && x.ScoreTypeId == model.ScoreTypeId, true)
				.FirstOrDefaultAsync();

			if (subjectScoreType == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			//kiểm tra xem có bao nhiu cột điểm đã được nhập cho học viên
			var countScoresOfStudent = await _unitOfWork.Score
				.Get(x => x.ApplicationUserId == model.StudentId && x.SubjectId == model.SubjectId && x.ClassId == model.ClassId && x.ScoreTypeId == model.ScoreTypeId, true)
				.CountAsync();

			//thêm điểm
			if (countScoresOfStudent < subjectScoreType.MandatoryScoreColumn)
			{
				Score newScore = new()
				{
					ScoreOfStudent = model.ScoreOfStudent,
					ScoreTypeId = model.ScoreTypeId,
					SubjectId = model.SubjectId,
					ClassId = model.ClassId,
					ApplicationUserId = model.StudentId,
				};

				_unitOfWork.Score.Add(newScore);
				_unitOfWork.Save();

				_res.Messages = "Thêm điểm thành công";
			}
			else
			{
				//báo lỗi đủ số cột điểm
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Đã đủ số cột điểm, số cột điểm là {subjectScoreType.MandatoryScoreColumn}." } }
					};
				_res.IsSuccess = false;
			}
			return _res;
		}

		public async Task<ApiResponse<object>> AddScoreForStudentsAsync(AddScoreForStudentsRequestDTO model)
		{
			if (model.ScoreTypeId == 0 || model.ClassId == 0 || model.SubjectId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			//lấy thông tin lớp
			var classInfor = await _unitOfWork.Class.Get(x => x.Id == model.ClassId, true).FirstOrDefaultAsync();

			if (classInfor == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (classInfor.FinalizeStudentScores)
			{
				//báo lỗi đã chốt điểm
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Đã chốt điểm" } }
					};
				_res.IsSuccess = false;
				return _res;
			}

			//lấy mã khóa đào tạo
			var trainingCourseId = classInfor.TrainingCourseId;

			//lấy loại điểm môn để biết số cột điẻm
			var subjectScoreType = await _unitOfWork.SubjectScoreType
				.Get(x => x.SubjectId == model.SubjectId && x.TrainingCourseId == trainingCourseId && x.ScoreTypeId == model.ScoreTypeId, true)
				.FirstOrDefaultAsync();

			if (subjectScoreType == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			//lặp ra từng học viên
			foreach (var student in model.StudentScores)
			{
				//kiểm tra xem có bao nhiu cột điểm đã được nhập cho học viên
				var countScoresOfStudent = await _unitOfWork.Score
				 .Get(x => x.ApplicationUserId == student.StudentId && x.SubjectId == model.SubjectId && x.ClassId == model.ClassId && x.ScoreTypeId == model.ScoreTypeId, true)
				 .CountAsync();

				//thêm điểm
				if (countScoresOfStudent < subjectScoreType.MandatoryScoreColumn)
				{
					Score newScore = new()
					{
						ScoreOfStudent = student.Score,
						ScoreTypeId = model.ScoreTypeId,
						SubjectId = model.SubjectId,
						ClassId = model.ClassId,
						ApplicationUserId = student.StudentId,
					};

					_unitOfWork.Score.AddAsync(newScore);
				}
				else
				{
					//báo lỗi đủ số cột điểm
					_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Đã đủ số cột điểm, số cột điểm là {subjectScoreType.MandatoryScoreColumn}." } }
					};
					_res.IsSuccess = false;
					return _res;
				}
			}
			_unitOfWork.SaveAsync();

			_res.Messages = "Thêm điểm thành công";
			return _res;
		}

		public async Task<ApiResponse<object>> UpdateScoreForStudentAsync(UpdateScoresForStudentRequestDTO model)
		{
			if (model.ClassId == 0 || string.IsNullOrEmpty(model.StudentId))
			{
				_res.IsSuccess = false;
				return _res;
			}

			//lấy thông tin lớp
			var classInfor = await _unitOfWork.Class.Get(x => x.Id == model.ClassId, true).FirstOrDefaultAsync();

			if (classInfor == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (classInfor.FinalizeStudentScores)
			{
				//báo lỗi đã chốt điểm
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Đã chốt điểm" } }
					};
				_res.IsSuccess = false;
				return _res;
			}

			//lấy mã khóa đào tạo
			var trainingCourseId = classInfor.TrainingCourseId;

			//lấy loại điểm môn để biết số cột điẻm
			var subjectScoreType = await _unitOfWork.SubjectScoreType
				.Get(x => x.SubjectId == model.SubjectId && x.TrainingCourseId == trainingCourseId && x.ScoreTypeId == model.ScoreTypeId, true)
				.FirstOrDefaultAsync();

			if (subjectScoreType == null)
			{
				//báo lỗi chưa có loại điểm môn
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Chưa có loại điểm môn" } }
					};
				_res.IsSuccess = false;
				return _res;
			}

			if (model.StudentScores.Count > subjectScoreType.MandatoryScoreColumn)
			{
				//báo lỗi quá số cột điểm
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Qúa số cột điểm, số cột điểm là {subjectScoreType.MandatoryScoreColumn}." } }
					};
				_res.IsSuccess = false;
				return _res;
			}

			//lấy các điểm của học viên
			var studentScores = await _unitOfWork.Score
				.Get(x => x.ApplicationUserId == model.StudentId && x.ClassId == model.ClassId && x.SubjectId == model.SubjectId && x.ScoreTypeId == model.ScoreTypeId, true)
				.ToListAsync();

			// trường hợp nhập chưa đủ cột điểm quy định trong bảng SubjectScoreType, ở form cập nhật điểm luôn hiển thị đủ số ô cho cột bắc buộc,
			// => thì sẽ tìm để xác dịnh các cột đã có sẽ cập nhật, các cột chưa có sẽ được thêm. 
			if (studentScores.Count < model.StudentScores.Count)
			{
				//cập nhật cột đẫ có + thêm cột chưa có vào bảng Score
				for (int i = 0; i < model.StudentScores.Count; i++)
				{
					if (i < studentScores.Count)
					{
						//cập nhật theo điểm mới
						studentScores[i].ScoreOfStudent = model.StudentScores[i];

						_unitOfWork.Score.Update(studentScores[i]);
					}
					else
					{
						//thêm điểm mới
						Score newScore = new()
						{
							ScoreOfStudent = model.StudentScores[i],
							ScoreTypeId = model.ScoreTypeId,
							SubjectId = model.SubjectId,
							ClassId = model.ClassId,
							ApplicationUserId = model.StudentId,
						};

						_unitOfWork.Score.Add(newScore);
					}
				}
			}
			else if (studentScores.Count == model.StudentScores.Count)
			{
				for (int i = 0; i < model.StudentScores.Count; i++)
				{

					//cập nhật theo điểm mới
					studentScores[i].ScoreOfStudent = model.StudentScores[i];

					_unitOfWork.Score.Update(studentScores[i]);
				}
			}

			_unitOfWork.Save();

			_res.Messages = "Cập nhật điểm thành công";
			return _res;
		}

		public async Task<ApiResponse<GetScoreOfClassResponseDTO>> GetScoreOfClassAsync(int classId, int subjectId)
		{
			ApiResponse<GetScoreOfClassResponseDTO> res = new();
			if (classId == 0)
			{
				res.IsSuccess = false;
				return res;
			}

			//lấy thông tin lớp
			var classInfor = await _unitOfWork.Class.Get(x => x.Id == classId, true).FirstOrDefaultAsync();

			if (classInfor == null)
			{
				res.IsSuccess = false;
				return res;
			}

			//lấy mã khóa đào tạo
			var trainingCourseId = classInfor.TrainingCourseId;

			//lấy điểm của các học viên trong lớp
			var scoresOfClass = await _unitOfWork.Score.Get(x => x.ClassId == classId && x.SubjectId == subjectId, true)
				.Include(x => x.ScoreType)
				.ToListAsync();

			//Lấy các môn mà lớp có và các cột điểm của môn
			var subjectScoreTypeOfClass = await _unitOfWork.SubjectScoreType
				.Get(x => x.SubjectId == subjectId && x.TrainingCourseId == trainingCourseId, true)
				.Include(x => x.ScoreType)
				.ToListAsync();

			res.Result.Scores = scoresOfClass;
			res.Result.SubjectScoreTypes = subjectScoreTypeOfClass;
			return res;
		}

		public async Task<ApiResponse<object>> FinalizeStudentScoresAsync(int classId)
		{
			if (classId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var classInDb = await _unitOfWork.Class.Get(x => x.Id == classId, true).FirstOrDefaultAsync();

			if (classInDb == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			classInDb.FinalizeStudentScores = true;

			_unitOfWork.Class.Update(classInDb);
			_unitOfWork.Save();

			_res.Messages = "Đã chốt điểm";
			return _res;
		}

		public async Task<ApiResponse<GetScoresOfStudentResponseDTO>> GetScoresOfStudentAsync(int classId, string studentId)
		{
			ApiResponse<GetScoresOfStudentResponseDTO> res = new();
			if (classId == 0 || string.IsNullOrEmpty(studentId))
			{
				res.IsSuccess = false;
				return res;
			}

			//lấy điểm của học viên theo lớp
			var studentScores = await _unitOfWork.Score
				.Get(x => x.ClassId == classId && x.ApplicationUserId == studentId, true)
				.Include(x => x.Subject)
				.Include(x => x.ScoreType)
				.ToListAsync();

			res.Result.Scores = studentScores;
			return res;
		}
	}
}