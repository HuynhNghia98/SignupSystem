using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.ScoreType;
using SignupSystem.Models.Response;
using SignupSystem.Services.ScoreType.Interface;

namespace SignupSystem.Services.ScoreType
{
	public class ScoreTypeService : IScoreTypeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public ScoreTypeService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}
		public async Task<ApiResponse<GetScoreTypesResponseDTO>> GetScoreTypesAsync()
		{
			var scoreTypes = await _unitOfWork.ScoreType.GetAll().ToListAsync();

			ApiResponse<GetScoreTypesResponseDTO> res = new();
			res.Result.ScoreTypes = scoreTypes;

			return res;
		}

		public async Task<ApiResponse<Models.ScoreType>> GetScoreTypeAsync(int scoreTypeId)
		{
			var scoreTypeInDb = await _unitOfWork.ScoreType.Get(x => x.Id == scoreTypeId, true).FirstOrDefaultAsync();

			ApiResponse<Models.ScoreType> res = new();

			if (scoreTypeInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = scoreTypeInDb;
			}
			return res;
		}

		public ApiResponse<object> AddScoreTypeAsync(AddOrUpdateScoreTypeRequestDTO model)
		{
			Models.ScoreType newScoreType = new()
			{
				NameType = model.ScoreTypeName,
				Coefficient = model.Coefficient,
			};

			_unitOfWork.ScoreType.Add(newScoreType);
			_unitOfWork.Save();

			_res.Messages = "Thêm loại điểm thành công";
			return _res;
		}

		public async Task<ApiResponse<object>> UpdateScoreTypeAsync(int scoreTypeId, AddOrUpdateScoreTypeRequestDTO model)
		{
			if (scoreTypeId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var scoreTypeInDb = await _unitOfWork.ScoreType.Get(x => x.Id == scoreTypeId, true).FirstOrDefaultAsync();

			if (scoreTypeInDb == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			scoreTypeInDb.NameType = model.ScoreTypeName;
			scoreTypeInDb.Coefficient = model.Coefficient;

			_unitOfWork.ScoreType.Update(scoreTypeInDb);
			_unitOfWork.Save();

			_res.Messages = "Đã cập nhật loại điểm thành công";
			return _res;

		}

		public async Task<ApiResponse<object>> DeleteScoreTypeAsync(int scoreTypeId)
		{
			if (scoreTypeId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var scoreTypeToDelete = await _unitOfWork.ScoreType.Get(x => x.Id == scoreTypeId, true).FirstOrDefaultAsync();

			if (scoreTypeToDelete == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.ScoreType.Remove(scoreTypeToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa loại điểm thành công";
			return _res;
		}
	}
}
