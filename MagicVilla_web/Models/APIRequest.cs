using static MagicVilla_Utility.SD;

namespace MagicVilla_web.Models
{
    public class APIRequest
    {
        public ApiType Type { get; set; } = ApiType.GET;
        public string Url { get;set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
