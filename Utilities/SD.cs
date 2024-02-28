using System.Collections.ObjectModel;

namespace SignupSystem.Utilities
{
	public class SD
	{
		//Role
		public const string Role_Admin = "Quản trị";
		public const string Role_RegistrationDepartment = "Bộ phận ghi danh";
		public const string Role_Accountant = "Bộ phận kế toán";
		public const string Role_BoardOfManager = "Ban giám đốc";
		public const string Role_Lecturer = "Giảng viên";
		public const string Role_Student = "Học viên";

		//Payment Status
		public const string Payment_UnPaid = "Chưa thanh toán";
		public const string Payment_Paid = "Đã thanh toán";

		//claim
		public const string Claim_ViewClassList = "Xem danh sách lớp";
		public const string Claim_ViewStudentListInClass = "Danh sách học sinh trong lớp";
		public const string Claim_ViewSubjectListInClass = "Danh sách môn học trong lớp";
		public const string Claim_AddEditDeleteClass = "Thêm xóa sửa lớp";
		public const string Claim_ViewAllTrainingManagers = "Xem tất cả quản lí đào tạo";
		public const string Claim_AddEditDeleteTrainingManager = "Thêm xóa sửa quản lí đào tạo";
		public const string Claim_ViewAllLecturers = "Xem tất cả danh sách giảng viên";
		public const string Claim_AddEditDeleteLecturer = "Thêm xóa sửa giảng viên";
		public const string Claim_GenerateReports = "Xuất báo cáo";
		public const string Claim_ViewTeachingSchedule = "Xem lịch giảng dạy";
		public const string Claim_AddEditDeleteTeachingSchedule = "Thêm xóa sửa lịch giảng dạy";
		public const string Claim_ViewScores = "Xem điểm";
		public const string Claim_UpdateScores = "Cập nhật điểm";
		public const string Claim_ViewAllScoreTypes = "Xem tất cả loại điểm";
		public const string Claim_AddEditDeleteScoreType = "Thêm xóa sửa loại điểm";
		public const string Claim_ViewAllStudents = "Xem tất cả danh sách học viên";
		public const string Claim_CancelCourseRegistration = "Huỷ đăng ký môn học";
		public const string Claim_AddEditDeleteStudent = "Thêm xóa sửa học viên";
		public const string Claim_ViewUserList = "Xem danh sách người dùng";
		public const string Claim_AddEditDeleteUserRole = "Thêm xóa sửa phân quyền người dùng";
		public const string Claim_ViewHolidaySchedule = "Danh sách lịch nghỉ";
		public const string Claim_AddEditDeleteHolidaySchedule = "Thêm xóa sửa lịch nghỉ";

		//claims
		public static List<string> ClaimList = new List<string>()
		{
			Claim_ViewClassList,
			Claim_ViewStudentListInClass,
			Claim_ViewSubjectListInClass,
			Claim_AddEditDeleteClass,
			Claim_ViewAllTrainingManagers,
			Claim_AddEditDeleteTrainingManager,
			Claim_ViewAllLecturers,
			Claim_AddEditDeleteLecturer,
			Claim_GenerateReports,
			Claim_ViewTeachingSchedule,
			Claim_AddEditDeleteTeachingSchedule,
			Claim_ViewScores,
			Claim_UpdateScores,
			Claim_ViewAllScoreTypes,
			Claim_AddEditDeleteScoreType,
			Claim_ViewAllStudents,
			Claim_CancelCourseRegistration,
			Claim_AddEditDeleteStudent,
			Claim_ViewUserList,
			Claim_AddEditDeleteUserRole,
			Claim_ViewHolidaySchedule,
			Claim_AddEditDeleteHolidaySchedule
		};
	}
}
