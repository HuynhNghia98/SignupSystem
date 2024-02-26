using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;
using SignupSystem.Models.DTO.Revenue;
using SignupSystem.Models.DTO.Student;
using SignupSystem.Models.Response;
using SignupSystem.Services.Revenue.Interface;
using SignupSystem.Utilities;

namespace SignupSystem.Services.Revenue
{
	public class RevenueService : ControllerBase, IRevenueService
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApiResponse<object> _res;
		public RevenueService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_res = new();
		}

		public async Task<ApiResponse<object>> AddEmployeeSalaryAsync(string lecturerId, AddCalculateEmployeeSalaryRequestDTO model)
		{
			if (ModelState.IsValid)
			{
				if (string.IsNullOrEmpty(lecturerId))
				{
					_res.IsSuccess = false;
					return _res;
				}

				//kiểm tra xem đã tính lương cho giảng viên trong tháng chưa
				var salaryInDb = await _unitOfWork.Salary
					.Get(x => x.SalaryDay.Month == DateTime.Now.Month, true)
					.FirstOrDefaultAsync();

				if (salaryInDb != null)
				{
					_res.IsSuccess = false;
					_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Đã có phiếu lương của giảng viên trong tháng {DateTime.Now.Month}" } }
					};
					return _res;
				}

				//thêm mới
				Salary newSalary = new()
				{
					SalaryOfEmployee = model.Salary,
					SalaryDay = DateTime.Now,
					AllowanceName = model.AllowanceName,
					Allowance = model.Allowance,
					Notes = model.Notes,
					ApplicationUserId = lecturerId,
					TrainingCourseId = model.TrainingCourseId,
				};

				_unitOfWork.Salary.Add(newSalary);
				_unitOfWork.Save();

				_res.Messages = "Đã thêm bảng lương thành công";
				return _res;
			}
			_res.IsSuccess = false;
			return _res;
		}
		public async Task<ApiResponse<GetSalaryResponseDTO>> CalculateEmployeeSalaryAsync(CalculateEmployeeSalaryRequestDTO model)
		{
			ApiResponse<GetSalaryResponseDTO> res = new();
			//lấy tổng số lớp mà giảng viên đã dạy theo khóa đào tạo
			var assignClassTeachesOfLecturer = await _unitOfWork.AssignClassTeach
				.Get(x => x.ApplicationUserId == model.LecturerId && x.Class.TrainingCourseId == model.TrainingCourseId, true)
				.Include(x => x.Class)
				.ToListAsync();

			//lấy danh sách lớp
			var classes = assignClassTeachesOfLecturer
				.Select(x => x.Class)
				.ToList();

			int studentTotal = 0;

			//tính tổng số học viên
			foreach (var item in classes)
			{
				studentTotal += await _unitOfWork.RegisterClass
					.Get(x => x.ClassId == item.Id && x.PaymentStatus == SD.Payment_Paid, true)
					.CountAsync();

			}

			//tổng doanh thu khóa học
			var payments = await _unitOfWork.RegisterClass
				.Get(x => x.Class.TrainingCourseId == model.TrainingCourseId && x.PaymentStatus == SD.Payment_Paid, true)
				.ToListAsync();

			double totalRevenue = 0;

			foreach (var item in payments)
			{
				if (item.Discount != null)
				{
					totalRevenue += item.Fee - (item.Fee * ((double)item.Discount / 100));
				}
				else
				{
					totalRevenue += item.Fee;
				}
			}

			double salary = ((model.PercentageOfSalaryPerStudent * studentTotal) / 100) * totalRevenue;

			res.Result.Salary = salary;
			return res;
		}
		public async Task<ApiResponse<object>> FinalizingSalariesForEmployeesAsync(int trainingCourseId)
		{
			if (trainingCourseId == 0)
			{
				_res.IsSuccess = false;
				return _res;
			}

			//lấy khóa đào tạo trong db
			var trainingCourseInDb = await _unitOfWork.TrainingCourse
				.Get(x => x.Id == trainingCourseId, true)
				.FirstOrDefaultAsync();

			if (trainingCourseInDb == null)
			{
				_res.IsSuccess = false;
				return _res;
			}

			if (DateTime.Now <= trainingCourseInDb.EndDate)
			{
				_res.IsSuccess = false;
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "error", new List<string> { $"Khóa đào tạo chưa kết thúc, ngày kết thúc: {trainingCourseInDb.EndDate}" } }
					};
				return _res;
			}

			//cập nhật chốt lương
			trainingCourseInDb.IsFinalizingSalary = true;

			_unitOfWork.TrainingCourse.Update(trainingCourseInDb);
			_unitOfWork.Save();

			_res.Messages = $"Đã chốt lương tất cả nhân viên trong khóa {trainingCourseInDb.Name} thành công";
			return _res;
		}
		public async Task<ApiResponse<GetStudentsResponseDTO>> SearchStudentsHavePaidTuitionAsync(string? search, int trainingCourseId)
		{
			ApiResponse<GetStudentsResponseDTO> res = new();

			var registerClassAndPaidOfStudent = new List<RegisterClass>();

			//tìm theo search
			if (!string.IsNullOrEmpty(search))
			{
				registerClassAndPaidOfStudent = await _unitOfWork.RegisterClass
				.Get(x => (x.ApplicationUser.UserCode.Contains(search) ||
						x.ApplicationUser.FirstName.Contains(search) ||
						x.ApplicationUser.LastName.Contains(search) ||
						x.ApplicationUser.Email.Contains(search) ||
						x.ApplicationUser.PhoneNumber.Contains(search)) &&
						x.PaymentStatus == SD.Payment_Paid, true)
				.Include(x => x.Class.TrainingCourse)
				.Include(x => x.ApplicationUser)
				.ToListAsync();
			}
			else
			{
				registerClassAndPaidOfStudent = await _unitOfWork.RegisterClass.Get(x => x.PaymentStatus == SD.Payment_Paid, true)
				.Include(x => x.Class.TrainingCourse)
				.Include(x => x.ApplicationUser)
				.ToListAsync();
			}

			var students = new List<ApplicationUser>();

			//tìm theo khóa đào tạo
			if (trainingCourseId != 0)
			{
				var registerClassAndPaidOfStudentWithTrainingCourseid = registerClassAndPaidOfStudent.Where(x => x.Class.TrainingCourseId == trainingCourseId);
				students = registerClassAndPaidOfStudentWithTrainingCourseid.Select(x => x.ApplicationUser).ToList();
			}
			else
			{
				students = registerClassAndPaidOfStudent.Select(x => x.ApplicationUser).ToList();
			}

			res.Result.Students = students;
			return res;
		}
	}
}
