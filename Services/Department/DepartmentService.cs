using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Department;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.Response;
using SignupSystem.Services.Department.Interfaces;

namespace SignupSystem.Services.Department
{
	public class DepartmentService: ControllerBase, IDepartmentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public DepartmentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}

		public async Task<ApiResponse<GetDepartmentsResponseDTO>> GetDepartmentsAsync()
		{
			var departments = await _unitOfWork.Department.GetAll().ToListAsync();

			ApiResponse<GetDepartmentsResponseDTO> res = new();
			res.Result.Departments = departments;

			return res;
		}

		public async Task<ApiResponse<Models.Department>> GetDepartmentAsync(int departmentId)
		{
			var departmentInDb = await _unitOfWork.Department.Get(x => x.Id == departmentId, true).FirstOrDefaultAsync();

			ApiResponse<Models.Department> res = new();

			if (departmentInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = departmentInDb;
			}
			return res;
		}

		public async Task<ApiResponse<GetDepartmentsResponseDTO>> SearchDepartmentsAsync(string search)
		{
			var departmentsInDb = await _unitOfWork.Department
				.Get(x => x.Name.Contains(search), true)
				.ToListAsync();

			ApiResponse<GetDepartmentsResponseDTO> res = new();
			res.Result.Departments = departmentsInDb;

			return res;
		}

		public ApiResponse<object> AddDepartmentAsync(AddOrUpdateDepartmentRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.Department newDepartment = new()
				{
					Name = model.DepartmentName,
				};

				_unitOfWork.Department.Add(newDepartment);
				_unitOfWork.Save();

				_res.Messages = "Thêm tổ bộ môn thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}

		public async Task<ApiResponse<object>> UpdateDepartmentAsync(int departmentId, AddOrUpdateDepartmentRequestDTO model)
		{
			if (departmentId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (ModelState.IsValid)
			{
				var departmentInDb = await _unitOfWork.Department.Get(x => x.Id == departmentId, true).FirstOrDefaultAsync();

				if (departmentInDb == null)
				{
					_res.IsSuccess = false;
					return _res;
				}

				departmentInDb.Name = model.DepartmentName;

				_unitOfWork.Department.Update(departmentInDb);
				_unitOfWork.Save();

				_res.Messages = "Đã cập nhật tổ bộ môn thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<object>> DeleteDepartmentAsync(int departmentId)
		{
			if (departmentId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var departmentToDelete = await _unitOfWork.Department.Get(x => x.Id == departmentId, true).FirstOrDefaultAsync();

			if (departmentToDelete == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.Department.Remove(departmentToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa tổ bộ môn thành công";
			return _res;
		}
	}
}
