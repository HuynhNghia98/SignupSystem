using Microsoft.AspNetCore.Identity;

namespace SignupSystem.DataAccess.DbInitializer
{
    public interface IDbInitializer
    {
		void Initializer();
		Task CreateRoleWithClaims(string roleName, List<string> claims);
		Task AddClaimToRole(IdentityRole role, string claimType, string claimValue);

	}
}
