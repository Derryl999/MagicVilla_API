using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.IServices;
using MagicVilla_Utility;
namespace MagicVilla_web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly string villaUrl;
        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                Type = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/v1/VillaNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id,string token)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.DELETE,
                Url = villaUrl + "/api/v1/VillaNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaNumberAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                Type = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/v1/VillaNumberAPI/" + dto.VillaNo,
                Token = token
            });
        }
    }
}
