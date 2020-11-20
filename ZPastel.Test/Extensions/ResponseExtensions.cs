using System.Net.Http;
using System.Threading.Tasks;

namespace ZPastel.Test.Extensions
{
    static class ResponseExtensions
    {
        public static async Task<T> Deserialize<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }
    }
}
