using MagicVilla_web.Models;

namespace MagicVilla_web.Services.IServices
{
    public interface IBaseService
    {
        Task<T> SendAsync<T>(APIRequest apiRequest);
        APIResponse responseModel { get; set; }
    }
    //public interface IBaseService<T> where T:class 
    //{
    //    Task<T> SendAsync(APIRequest apiRequest);
    //    APIResponse responseModel { get; set; }
    //}
}
