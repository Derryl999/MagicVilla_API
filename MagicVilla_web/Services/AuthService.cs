using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.IServices;
using MagicVilla_Utility;
namespace MagicVilla_web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private string url;
        public AuthService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient) {
            url = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.POST,
                Data = loginRequestDTO,
                Url = url + "/api/v1/userAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO registrationRequestDTO)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.POST,
                Data = registrationRequestDTO,
                Url = url + "/api/v1/userAuth/register"
            });
        }
    }
}
