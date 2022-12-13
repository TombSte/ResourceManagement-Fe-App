using Microsoft.Extensions.Options;
using ResourceManagement_Fe_App.Authentication;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResourceManagement_Fe_App.Helpers
{
	public interface IRmHttpClient
	{
		public Task<TResponse> GetAsync<TResponse>(string url);
	}
	public class RmHttpClient : IRmHttpClient
	{
		private readonly IAuthenticationHelper authHelper;
		//private readonly IHttpClientFactory ClientFactory;
		private readonly HttpClient apiClient; 
		public RmHttpClient(IAuthenticationHelper authHelper, HttpClient client, IOptions<ServerOptions> serverOptions)
		{
			this.authHelper = authHelper;
			apiClient = client;
			apiClient.BaseAddress = new Uri(serverOptions.Value.BaseUrl);

        }

		public async Task<TResponse> GetAsync<TResponse>(string url)
		{
			await SetToken();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var response = await this.apiClient.GetAsync(url);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<TResponse>(responseBody, options);
		}

		private async Task SetToken()
		{
			var token = await authHelper.GetJWTAsync();
			apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
