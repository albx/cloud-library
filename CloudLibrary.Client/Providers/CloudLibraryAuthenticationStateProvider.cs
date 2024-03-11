using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace CloudLibrary.Client.Providers;

public class CloudLibraryAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CloudLibraryAuthenticationStateProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("AuthClient");

            var userInfo = await httpClient.GetFromJsonAsync<UserInformation>(".auth/me");
            var clientPrincipal = userInfo!.ClientPrincipal;

            if (clientPrincipal is not null)
            {
                var identity = new ClaimsIdentity(clientPrincipal.IdentityProvider);
                if (!string.IsNullOrEmpty(clientPrincipal.UserId))
                {
                    identity.AddClaim(new(ClaimTypes.NameIdentifier, clientPrincipal.UserId));
                }
                if (!string.IsNullOrEmpty(clientPrincipal.UserDetails))
                {
                    identity.AddClaim(new(ClaimTypes.Name, clientPrincipal.UserDetails));
                }
                if (clientPrincipal.UserRoles.Any())
                {
                    identity.AddClaims(clientPrincipal.UserRoles
                        .Where(r => !r.Equals("anonymous", StringComparison.InvariantCultureIgnoreCase))
                        .Select(r => new Claim(ClaimTypes.Role, r)));
                }
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }
        catch 
        {
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }

    internal class UserInformation
    {
        public ClientPrincipal? ClientPrincipal { get; set; }
    }

    internal record ClientPrincipal
    {
        public string IdentityProvider { get; init; } = string.Empty;

        public string UserId { get; init; } = string.Empty;

        public string UserDetails { get; init; } = string.Empty;

        public string[] UserRoles { get; init; } = [];
    }
}
