using AntDesign;
using Microsoft.Extensions.Options;
using ResourceManagement_Fe_App.Authentication;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ResourceManagement_Fe_App.Helpers
{
    public class HttpResult<T>
    {
        public bool Success { get; set; }
        public T Value { get; set; }
    }

	public interface IRmHttpClient
	{
		public Task<HttpResult<TResponse>> GetAsync<TResponse>(string url);
		public Task PostAsync<TBody>(string url, TBody body);
		public Task<HttpResult<TResponse>> PostAsync<TBody, TResponse>(string url, TBody body);
	}
	public class RmHttpClient : IRmHttpClient
	{
		private readonly IAuthenticationHelper authHelper;
		//private readonly IHttpClientFactory ClientFactory;
		private readonly HttpClient apiClient;
        private readonly NotificationService _notice;

        public RmHttpClient(
            IAuthenticationHelper authHelper, HttpClient client, IOptions<ServerOptions> serverOptions, NotificationService notice)
        {
            this.authHelper = authHelper;
            apiClient = client;
            apiClient.BaseAddress = new Uri(serverOptions.Value.BaseUrl);
            _notice = notice;
        }

        private void ErrorNotification(string text = null)
        {
            _notice.Error(new()
            {
                Message = "Attenzione",
                Description = text != null ? text : "Si è verificato un errore. \nRiprovare."
            });
        }

        public async Task<HttpResult<TResponse>> GetAsync<TResponse>(string url)
		{
			await SetToken();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                var response = await this.apiClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return new HttpResult<TResponse>()
                {
                    Success = true,
                    Value = JsonSerializer.Deserialize<TResponse>(responseBody, options)
                };
            }
            catch (Exception)
            {
                return new HttpResult<TResponse>()
                {
                    Success = false,
                    Value = default(TResponse)
                };
            }
            
		}

        public async Task PostAsync<TBody>(string url, TBody body)
        {
            await SetToken();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var response = await this.apiClient.PostAsJsonAsync(url, body);
            if (!response.IsSuccessStatusCode)
            {
                ErrorNotification();
                return;
            }
        }

        public async Task<HttpResult<TResponse>> PostAsync<TBody, TResponse>(string url, TBody body)
        {
            await SetToken();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var response = await this.apiClient.PostAsJsonAsync(url, body);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return new HttpResult<TResponse>()
                {
                    Success = true,
                    Value = JsonSerializer.Deserialize<TResponse>(responseBody, options)
                };
            }
            catch (Exception)
            {
                return new HttpResult<TResponse>()
                {
                    Success = false,
                    Value = default(TResponse)
                };
            }

        }

        private async Task SetToken()
		{
			var token = await authHelper.GetJWTAsync();
			apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
