using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core3_Framework.Contracts.Infrastructure
{
    public interface IClaimPrincipalManager
    {
        Boolean IsAuthenticated { get; }
        String UserName { get; }
        String UserId { get; }

        ClaimsPrincipal User { get; }

        Task<int> LoginAsync(String email, String password);
        Task LogoutAsync();
        Task RenewTokenAsync(String jwtToken);
    }
}
