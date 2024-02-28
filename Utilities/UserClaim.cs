using Microsoft.AspNetCore.Identity;
using SignupSystem.Models;
using System.Security.Claims;

namespace SignupSystem.Utilities
{
	public static class UserClaim
	{
		public static void AddClaimsToUser(ApplicationUser user, UserManager<ApplicationUser> userManager, List<string> claims)
		{
			// Lấy danh sách các claim hiện tại của người dùng
			var existingClaims = userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

			foreach (var claim in claims)
			{
				// Kiểm tra xem claim đã tồn tại cho người dùng chưa
				if (!existingClaims.Any(c => c.Type == claim && c.Value == "True"))
				{
					// Thêm claim mới cho người dùng
					userManager.AddClaimAsync(user, new Claim(claim, "True")).GetAwaiter().GetResult();
				}
			}
		}

		public static void UpdateClaimsToUser(ApplicationUser user, UserManager<ApplicationUser> userManager, Dictionary<string, bool> newClaims)
		{
			// Lấy danh sách các claim hiện tại của người dùng
			var existingClaims = userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

			foreach (var claim in newClaims)
			{
				// Kiểm tra xem claim đã tồn tại cho người dùng chưa
				var existingClaim = existingClaims.FirstOrDefault(c => c.Type == claim.Key);

				if (existingClaim != null && existingClaim.Value != claim.Value.ToString())
				{
					// Tạo một Claim object mới
					var newClaim = new Claim(claim.Key, claim.Value.ToString());

					// Nếu claim đã tồn tại, thay đổi giá trị claim
					userManager.ReplaceClaimAsync(user, existingClaim, newClaim).GetAwaiter().GetResult();
				}
			}
		}
	}
}
