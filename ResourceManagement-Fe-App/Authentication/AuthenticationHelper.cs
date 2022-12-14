using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.CompilerServices;

namespace ResourceManagement_Fe_App.Authentication
{ 
	public interface IAuthenticationHelper
	{
		public Task<bool> IsLoggedAsync();
		public UserData User { get; }
        public Task<string> GetJWTAsync();
        
    }

    public interface IAuthenticationHelperWriter
	{
		public Task SetLocalAsync(string email, string jwt);
        public Task Logout();
    }

    public class AuthenticationHelper : IAuthenticationHelper, IAuthenticationHelperWriter
    {
		private readonly IJSRuntime jsr;
		public AuthenticationHelper(IJSRuntime jsr)
		{
			this.jsr = jsr;
		}

		public UserData User => throw new NotImplementedException();

        public async Task<string> GetJWTAsync()
        {
            string jwt = await jsr.InvokeAsync<string>("localStorage.getItem", "userjwt").ConfigureAwait(false);
            return jwt;

		}

        public async Task RefreshLocalEmailAsync()
        {
            string email = await jsr.InvokeAsync<string>("localStorage.getItem", "useremail").ConfigureAwait(false);
        }

        public async Task SetLocalAsync(string email, string jwt)
        {
            await jsr.InvokeVoidAsync("localStorage.setItem", "userjwt", $"{jwt}").ConfigureAwait(false);
            await jsr.InvokeVoidAsync("localStorage.setItem", "useremail", $"{email}").ConfigureAwait(false);
        }

        public async Task Logout()
        {
            await jsr.InvokeVoidAsync("localStorage.setItem", "userjwt", "").ConfigureAwait(false);
            await jsr.InvokeVoidAsync("localStorage.setItem", "useremail", "").ConfigureAwait(false);
        }

        public async Task<bool> IsLoggedAsync()
		{
            var jwt = await GetJWTAsync();
            var jwtNull = string.IsNullOrWhiteSpace(jwt);
            if (jwtNull) return false;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(jwt);

            var expiration = jsonToken.Claims.FirstOrDefault(i => i.Type.Equals("exp"));
            if(expiration == null) return false;
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expiration.Value));
            return dateTimeOffset.UtcDateTime > DateTime.UtcNow;
        }
	}
}
