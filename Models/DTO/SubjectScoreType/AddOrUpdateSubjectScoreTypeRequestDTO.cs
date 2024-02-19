using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.SubjectScoreType
{
	public class AddOrUpdateSubjectScoreTypeRequestDTO
	{
		[Required(ErrorMessage = "Chọn khóa đào tạo")]
		public int TraininngCourseId { get; set; }
		[Required(ErrorMessage = "Chọn môn học")]
		public int SubjectId { get; set; }
		[Required(ErrorMessage = "Chọn loại điẻm")]
		public int ScoreTypeId { get; set; }
		[Required(ErrorMessage = "Nhập số cột điẻm")]
		public int ScoreColumn { get; set; }
		[Required(ErrorMessage = "Nhập số cột điẻm bắt buộc")]
		public int MandatoryScoreColumn { get; set; }
	}
}
