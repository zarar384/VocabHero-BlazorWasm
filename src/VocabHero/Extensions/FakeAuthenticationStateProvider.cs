using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace VocabHero.Extensions
{
    public class FakeAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Fake User"),
            }, "Fake authentication");

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
