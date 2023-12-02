using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorPortal.Auth
{
    public class EDSAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
            var usuario = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Rico3d"),
                new Claim(ClaimTypes.Role, "Admin")
            },"eds");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuario)));
        }
    }
}
