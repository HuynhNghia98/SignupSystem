using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupSystem.Models
{
	public class ScoreType
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string NameType { get; set; } = string.Empty;
		//hệ số
		[Required]
		public int Coefficient { get; set; }

		public ICollection<Score>? Scores { get; set; }
	}
}
