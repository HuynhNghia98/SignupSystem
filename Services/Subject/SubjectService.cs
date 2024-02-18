using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models.DTO.Subject;
using SignupSystem.Models.DTO.TrainingCourse;
using SignupSystem.Models.Response;
using SignupSystem.Services.Subject.Interfaces;

namespace SignupSystem.Services.Subject
{
	public class SubjectService : ControllerBase, ISubjectService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public SubjectService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}
		public async Task<ApiResponse<GetSubjectsResponseDTO>> GetSubjectsAsync()
		{
			var subjects = await _unitOfWork.Subject.GetAll().Include(x=>x.Faculty).Include(x => x.Department).ToListAsync();

			ApiResponse<GetSubjectsResponseDTO> res = new();
			res.Result.Subjects = subjects;

			return res;
		}

		public async Task<ApiResponse<Models.Subject>> GetSubjectAsync(int id)
		{
			var subjectInDb = await _unitOfWork.Subject.Get(x => x.Id == id, true).FirstOrDefaultAsync();

			ApiResponse<Models.Subject> res = new();

			if (subjectInDb == null)
			{
				res.IsSuccess = false;
			}
			else
			{
				res.Result = subjectInDb;
			}
			return res;
		}

		public async Task<ApiResponse<GetSubjectsResponseDTO>> SearchSubjectsAsync(string search)
		{
			var subjectsInDb = await _unitOfWork.Subject.Get(x => x.SubjectCode.Contains(search) ||
																				x.Name.Contains(search)
																				, true)
																			.Include(x=>x.Faculty).Include(x => x.Department).ToListAsync();

			ApiResponse<GetSubjectsResponseDTO> res = new();
			res.Result.Subjects = subjectsInDb;

			return res;
		}

		public ApiResponse<object> AddSubjectAsync(AddOrUpdateSubjectRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				Models.Subject newSubject = new()
				{
					Name = model.ClassName,
					Details = model.Detail,
					FacultyId = model.FacultyId,
					DepartmentId = model.DepartmentId,
				};
				
				_unitOfWork.Subject.Add(newSubject);
				_unitOfWork.Save();

				_res.Messages = "Thêm môn học thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}

		public async Task<ApiResponse<object>> UpdateSubjectAsync(int subjectId, AddOrUpdateSubjectRequestDTO model)
		{
			if (subjectId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (ModelState.IsValid)
			{
				var subjectInDb = await _unitOfWork.Subject.Get(x => x.Id == subjectId, true).FirstOrDefaultAsync();

				if (subjectInDb == null)
				{
					_res.IsSuccess = false;
					return _res;
				}

				subjectInDb.Name = model.ClassName;
				subjectInDb.Details = model.Detail;
				subjectInDb.FacultyId = model.FacultyId;
				subjectInDb.DepartmentId = model.DepartmentId;

				_unitOfWork.Subject.Update(subjectInDb);
				_unitOfWork.Save();

				_res.Messages = "Đã cập nhật môn học thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}

		public async Task<ApiResponse<object>> DeleteSubjectAsync(int subjectId)
		{
			if (subjectId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			var subjectToDelete = await _unitOfWork.Subject.Get(x => x.Id == subjectId, true).FirstOrDefaultAsync();

			if (subjectToDelete == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.Subject.Remove(subjectToDelete);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa môn học thành công";
			return _res;
		}

	}
}
