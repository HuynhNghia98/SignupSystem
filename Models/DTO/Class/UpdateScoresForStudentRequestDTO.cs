using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Class
{
	public class UpdateScoresForStudentRequestDTO
	{
		[Required(ErrorMessage = "Chọn lớp học")]
		public int ClassId { get; set; }
		[Required(ErrorMessage = "Chọn môn học")]
		public int SubjectId { get; set; }
		[Required(ErrorMessage = "Chọn loại điểm")]
		public int ScoreTypeId { get; set; }
		[Required(ErrorMessage = "Chọn học viên")]
		public string StudentId { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập điểm học viên")]
		public List<double> StudentScores { get; set; } = new();
	}
}
