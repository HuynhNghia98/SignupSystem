using System.ComponentModel.DataAnnotations;

namespace SignupSystem.Models.DTO.ScoreType
{
	public class AddOrUpdateScoreTypeRequestDTO
	{
		[Required(ErrorMessage = "Nhập tên loại điểm")]
		public string ScoreTypeName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập hệ số")]
		public int Coefficient { get; set; }
	}
}
