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
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                Type = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/VillaNumberAPI"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaNumberAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                Type = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/VillaNumberAPI/" + dto.VillaNo

            });
        }
    }
}
