using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models.Response;
using SignupSystem.Models;
using SignupSystem.Services.Class.Interfaces;
using SignupSystem.Models.DTO.Class;
using Microsoft.EntityFrameworkCore;

namespace SignupSystem.Services.Class
{
    public class ClassService : ControllerBase, IClassService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public ClassService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}

		public async Task<ApiResponse<GetClassesResponseDTO>> GetClassesAsync()
		{
			var classes = await _unitOfWork.Class.GetAll().ToListAsync();

			ApiResponse<GetClassesResponseDTO> res = new();
			res.Result.Classes = classes;

			return res;
		}
		public async Task<ApiResponse<Models.Class>> GetClassAsync(int id)
		{
			var classInDb = await _unitOfWork.Class.Get(x => x.Id == id, true).FirstOrDefaultAsync();

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
		public ApiResponse<object> AddClassAsync(AddOrUpdateClassRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.Class newClass = new()
				{
					Name = model.ClassName,
					Fee = model.Fee,
				};

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

				classInDb.Name = model.ClassName;
				classInDb.Fee = model.Fee;

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
				return _res;
			}

			_unitOfWork.Class.Remove(classToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa lớp thành công";
			return _res;
		}
	}
}