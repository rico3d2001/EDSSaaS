using BlazorPortal.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorPortal.Auth
{
    public class TokenAutheticationProvider : AuthenticationStateProvider, IAuthorizeService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;
        public static readonly string tokenkey = "tokenKey";

        public TokenAutheticationProvider(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _js = jsRuntime;
            _http = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _js.GetFromLocalStorage(tokenkey);
                if (string.IsNullOrEmpty(token))
                {
                    return notAuthenticate;
                }

                return CreateAthenticationState(token);
            }
            catch (InvalidOperationException)
            {
            }

            return notAuthenticate;
            
        }

        private AuthenticationState CreateAthenticationState(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(string username, string password)
        {
            await _js.SetInLocalStorage(tokenkey, "token");
            var authState = CreateAthenticationState("token");
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await _js.RemoveItem(tokenkey);
            _http.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(notAuthenticate));
        }

        public Task<bool> Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        private AuthenticationState notAuthenticate => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}
