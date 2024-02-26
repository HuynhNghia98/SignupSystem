using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.TrainingCourse;
using SignupSystem.Models.Response;
using SignupSystem.Services.TrainningCourse.Interfaces;

namespace SignupSystem.Services.TrainningCourse
{
	public class TrainingCourseService : ControllerBase, ITrainingCourseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public TrainingCourseService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}

		public async Task<ApiResponse<GetTrainingCoursesResponseDTO>> GetTrainingCoursesAsync()
		{
			var trainingCourses = await _unitOfWork.TrainingCourse.GetAll().ToListAsync();

			ApiResponse<GetTrainingCoursesResponseDTO> res = new();
			res.Result.TrainingCourses = trainingCourses;

			return res;
		}
		public async Task<ApiResponse<TrainingCourse>> GetTrainingCourseAsync(int id)
		{
			var trainingCourseInDb = await _unitOfWork.TrainingCourse.Get(x => x.Id == id, true).FirstOrDefaultAsync();

			ApiResponse<Models.TrainingCourse> res = new();

			if (trainingCourseInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = trainingCourseInDb;
			}
			return res;
		}
		public async Task<ApiResponse<GetTrainingCoursesResponseDTO>> SearchTrainingCourseAsync(string search)
		{
			var trainingCourseInDb = await _unitOfWork.TrainingCourse.Get(x => x.TrainingCourseCode.Contains(search) ||
																				x.Name.Contains(search)
																				, true).ToListAsync();

			ApiResponse<GetTrainingCoursesResponseDTO> res = new();
			res.Result.TrainingCourses = trainingCourseInDb;

			return res;
		}
		public ApiResponse<object> AddTrainingCourseAsync(AddOrUpdateTrainingCourseRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.TrainingCourse newTrainingCourse = new()
				{
					TrainingCourseCode = model.TrainingCourseCode,
					Name = model.TrainingCourseName,
				};

				_unitOfWork.TrainingCourse.Add(newTrainingCourse);
				_unitOfWork.Save();

				_res.Messages = "Thêm khóa thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> UpdateTrainingCourseAsync(int id, AddOrUpdateTrainingCourseRequestDTO model)
		{
			if (id == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (ModelState.IsValid)
			{
				var trainingCourseInDb = await _unitOfWork.TrainingCourse.Get(x => x.Id == id, true).FirstOrDefaultAsync();

				if (trainingCourseInDb == null)
				{
					_res.IsSuccess = false;
					return _res;
				}

				trainingCourseInDb.TrainingCourseCode = model.TrainingCourseCode;
				trainingCourseInDb.Name = model.TrainingCourseName;

				_unitOfWork.TrainingCourse.Update(trainingCourseInDb);
				_unitOfWork.Save();

				_res.Messages = "Đã cập nhật khóa thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> DeleteTrainingCourseAsync(int id)
		{
			if (id == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var trainingCourseToDelete = await _unitOfWork.TrainingCourse.Get(x => x.Id == id, true).FirstOrDefaultAsync();

			if (trainingCourseToDelete == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.TrainingCourse.Remove(trainingCourseToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa khóa thành công";
			return _res;
		}
		public ApiResponse<object> CopyTrainingCourseAsync(CopyTraningCourseRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.TrainingCourse newTrainingCourse = new()
				{
					TrainingCourseCode = model.TrainingCourseCode,
					Name = model.TrainingCourseName,
					StartDate = model.StartDay,
					EndDate = model.EndDay,
				};

				_unitOfWork.TrainingCourse.Add(newTrainingCourse);
				_unitOfWork.Save();

				_res.Messages = "Đã sao chép khóa thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
	}
}
