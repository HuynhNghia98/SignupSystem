using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Faculty;
using SignupSystem.Models.DTO.SubjectScoreType;
using SignupSystem.Models.Response;
using SignupSystem.Services.SubjectScoreType.Interface;

namespace SignupSystem.Services.SubjectScoreType
{
	public class SubjectScoreTypeService : ControllerBase, ISubjectScoreTypeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public SubjectScoreTypeService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}

		public async Task<ApiResponse<GetSubjectScoreTypeResponseDTO>> GetSubjectScoreTypesAsync()
		{
			var subjectScoreTypes = await _unitOfWork.SubjectScoreType.GetAll().ToListAsync();

			ApiResponse<GetSubjectScoreTypeResponseDTO> res = new();
			res.Result.SubjectScoreTypes = subjectScoreTypes;

			return res;
		}
		public async Task<ApiResponse<Models.SubjectScoreType>> GetSubjectScoreTypeAsync(int id)
		{
			var subjectScoreTypeInDb = await _unitOfWork.SubjectScoreType.Get(x => x.Id == id, true).FirstOrDefaultAsync();

			ApiResponse<Models.SubjectScoreType> res = new();

			if (subjectScoreTypeInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = subjectScoreTypeInDb;
			}
			return res;
		}
		public async Task<ApiResponse<GetSubjectScoreTypeResponseDTO>> SearchSubjectScoreTypesAsync(string search)
		{
			var subjectScoreTypeInDb = await _unitOfWork.SubjectScoreType
			.Get(x => x.TrainingCourse.Name.Contains(search) || x.Subject.Name.Contains(search), true)
			.ToListAsync();

			ApiResponse<GetSubjectScoreTypeResponseDTO> res = new();
			res.Result.SubjectScoreTypes = subjectScoreTypeInDb;

			return res;
		}
		public ApiResponse<object> AddSubjectScoreTypeAsync(AddOrUpdateSubjectScoreTypeRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.SubjectScoreType newSubjectScoreType = new()
				{
					TrainingCourseId = model.TraininngCourseId,
					SubjectId = model.SubjectId,
					ScoreTypeId = model.ScoreTypeId,
					ScoreColumn = model.ScoreColumn,
					MandatoryScoreColumn = model.MandatoryScoreColumn,
				};

				_unitOfWork.SubjectScoreType.Add(newSubjectScoreType);
				_unitOfWork.Save();

				_res.Messages = "Thêm loại điểm môn thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> UpdateSubjectScoreTypeAsync(int id, AddOrUpdateSubjectScoreTypeRequestDTO model)
		{
			if (id == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (ModelState.IsValid)
			{
				var subjectScoreTypeInDb = await _unitOfWork.SubjectScoreType.Get(x => x.Id == id, true).FirstOrDefaultAsync();

				if (subjectScoreTypeInDb == null)
				{
					_res.IsSuccess = false;
					return _res;
				}

				subjectScoreTypeInDb.TrainingCourseId = model.TraininngCourseId;
				subjectScoreTypeInDb.SubjectId = model.SubjectId;
				subjectScoreTypeInDb.ScoreTypeId = model.ScoreTypeId;
				subjectScoreTypeInDb.ScoreColumn = model.ScoreColumn;
				subjectScoreTypeInDb.MandatoryScoreColumn = model.MandatoryScoreColumn;

				_unitOfWork.SubjectScoreType.Update(subjectScoreTypeInDb);
				_unitOfWork.Save();

				_res.Messages = "Đã cập nhật loại điểm môn thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> DeleteSubjectScoreTypeAsync(int id)
		{
			if (id == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var subjectScoreTypeToDelete = await _unitOfWork.SubjectScoreType.Get(x => x.Id == id, true).FirstOrDefaultAsync();

			if (subjectScoreTypeToDelete == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.SubjectScoreType.Remove(subjectScoreTypeToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa loại điểm thành công";
			return _res;
		}
	}
}
