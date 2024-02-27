using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Auth;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.DTO.Vacation;
using SignupSystem.Models.Response;
using SignupSystem.Services.Vacation.Interface;

namespace SignupSystem.Services.Vacation
{
	public class VacationService : ControllerBase, IVacationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public VacationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}

		public async Task<ApiResponse<GetVacationsResponseDTO>> GetVacationsAsync()
		{
			var vacations = await _unitOfWork.Vacation.GetAll().ToListAsync();

			ApiResponse<GetVacationsResponseDTO> res = new();
			res.Result.Vacations = vacations;

			return res;
		}
		public async Task<ApiResponse<Models.Vacation>> GetVacationAsync(int vacationId)
		{
			var vacationInDb = await _unitOfWork.Vacation.Get(x => x.Id == vacationId, true).FirstOrDefaultAsync();

			ApiResponse<Models.Vacation> res = new();

			if (vacationInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = vacationInDb;
			}
			return res;
		}
		public async Task<ApiResponse<GetVacationsResponseDTO>> SearchVacationsAsync(string search)
		{
			var vacationsInDb = await _unitOfWork.Vacation
				.Get(x => x.VacationName.Contains(search) || x.Reason.Contains(search), true)
				.ToListAsync();

			ApiResponse<GetVacationsResponseDTO> res = new();
			res.Result.Vacations = vacationsInDb;

			return res;
		}
		public ApiResponse<object> AddVacationAsync(AddOrUpdateVacationRequestDTO model)
		{
			Models.Vacation newVacation = new()
			{
				VacationName = model.VacationName,
				Reason = model.Reason,
				StartDay = model.StartDay,
				EndDay = model.EndDay
			};

			_unitOfWork.Vacation.Add(newVacation);
			_unitOfWork.SaveAsync();

			_res.Messages = "Thêm kỳ nghỉ thành công";
			return _res;
		}
		public async Task<ApiResponse<object>> UpdateVacationAsync(int vacationId, AddOrUpdateVacationRequestDTO model)
		{
			if (vacationId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var vacationInDb = await _unitOfWork.Vacation.Get(x => x.Id == vacationId, true).FirstOrDefaultAsync();

			if (vacationInDb == null)
			{
				_res.IsSuccess = false;
				_res.Errors = new Dictionary<string, List<string>>
				{
					{ "vacationId", new List<string> { $"Không thể tìm thấy kỳ nghỉ." }}
				};
				return _res;
			}

			vacationInDb.VacationName = model.VacationName;
			vacationInDb.Reason = model.Reason;
			vacationInDb.StartDay = model.StartDay;
			vacationInDb.EndDay = model.EndDay;

			_unitOfWork.Vacation.Update(vacationInDb);
			_unitOfWork.Save();

			_res.Messages = "Đã cập nhật kỳ nghỉ thành công";
			return _res;
		}
		public async Task<ApiResponse<object>> DeleteVacationAsync(int vacationId)
		{
			if (vacationId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var vacationToDelete = await _unitOfWork.Vacation.Get(x => x.Id == vacationId, true).FirstOrDefaultAsync();

			if (vacationToDelete == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.Vacation.Remove(vacationToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa kỳ nghỉ thành công";
			return _res;
		}
	}
}
