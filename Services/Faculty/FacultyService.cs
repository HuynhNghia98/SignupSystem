using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Department;
using SignupSystem.Models.DTO.Faculty;
using SignupSystem.Models.Response;
using SignupSystem.Services.Faculty.Interfaces;

namespace SignupSystem.Services.Faculty
{
	public class FacultyService : ControllerBase, IFacultyService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public FacultyService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}

		public async Task<ApiResponse<GetFacultiesResponseDTO>> GetFacultiesAsync()
		{
			var faculties = await _unitOfWork.Faculty.GetAll().ToListAsync();

			ApiResponse<GetFacultiesResponseDTO> res = new();
			res.Result.Faculties = faculties;

			return res;
		}

		public async Task<ApiResponse<Models.Faculty>> GetFacultyAsync(int facultyId)
		{
			var facultyInDb = await _unitOfWork.Faculty.Get(x => x.Id == facultyId, true).FirstOrDefaultAsync();

			ApiResponse<Models.Faculty> res = new();

			if (facultyInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = facultyInDb;
			}
			return res;
		}

		public async Task<ApiResponse<GetFacultiesResponseDTO>> SearchFacultiesAsync(string search)
		{
			var facultyInDb = await _unitOfWork.Faculty
				.Get(x => x.Name.Contains(search), true)
				.ToListAsync();

			ApiResponse<GetFacultiesResponseDTO> res = new();
			res.Result.Faculties = facultyInDb;

			return res;
		}

		public ApiResponse<object> AddFacultyAsync(AddOrUpdateFacultyRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.Faculty newFaculty = new()
				{
					FacultyCode = model.FacultyCode,
					Name = model.FacultyName,
				};

				_unitOfWork.Faculty.Add(newFaculty);
				_unitOfWork.Save();

				_res.Messages = "Thêm khoa thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> UpdateFacultyAsync(int facultyId, AddOrUpdateFacultyRequestDTO model)
		{
			if (facultyId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (ModelState.IsValid)
			{
				var facultyInDb = await _unitOfWork.Faculty.Get(x => x.Id == facultyId, true).FirstOrDefaultAsync();

				if (facultyInDb == null)
				{
					_res.IsSuccess = false;
					return _res;
				}

				facultyInDb.FacultyCode = model.FacultyCode;
				facultyInDb.Name = model.FacultyName;

				_unitOfWork.Faculty.Update(facultyInDb);
				_unitOfWork.Save();

				_res.Messages = "Đã cập nhật khoa thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}

		public async Task<ApiResponse<object>> DeleteFacultyAsync(int facultyId)
		{
			if (facultyId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var facultyToDelete = await _unitOfWork.Faculty.Get(x => x.Id == facultyId, true).FirstOrDefaultAsync();

			if (facultyToDelete == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.Faculty.Remove(facultyToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa khoa thành công";
			return _res;
		}
	}
}
