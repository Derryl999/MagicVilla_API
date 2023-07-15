using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.IServices;
using MagicVilla_Utility;
namespace MagicVilla_web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly string villaUrl;
        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                Type = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                Type = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                Type = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI/" + dto.Id

            });
        }
    }
}
