using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.Class
{
	public class AddScoreForStudentsRequestDTO
	{
		[Required(ErrorMessage = "Chọn môn học")]
		public int SubjectId { get; set; }
		[Required(ErrorMessage = "Chọn loại điểm số")]
		public int ScoreTypeId { get; set; }
		[Required(ErrorMessage = "Nhập điểm số")]
		public List<StudentScore> StudentScores { get; set; } = new();
		[Required(ErrorMessage = "Chọn lớp học")]
		public int ClassId { get; set; }

	
	}
}
