using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Lecturer
{
	public class AddTeachingAssignmentRequestDTO
	{
		[Required(ErrorMessage = "Chọn lớp học")]
		public int ClassId { get; set; }
		[Required(ErrorMessage = "Chọn môn học")]
		public int SubjectId { get; set; }
		[Required(ErrorMessage = "Chọn giảng viên")]
		public string LecturerId { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập phòng học")]
		public string Room { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập thứ")]
		public string DayOfWeek { get; set; } = string.Empty;

		// Giờ học
		[Required(ErrorMessage = "Nhập giờ bắt đầu học")]
		public TimeSpan StartTime { get; set; }
		[Required(ErrorMessage = "Nhập giờ kết thúc học")]
		public TimeSpan EndTime { get; set; }

		// Ngày học
		[Required(ErrorMessage = "Nhập ngày bắt đầu học")]
		public DateTime StartDay { get; set; }
		[Required(ErrorMessage = "Nhập ngày kết thúc học")]
		public DateTime EndDay { get; set; }

		public string? Detail { get; set; } = string.Empty;
	}
}
