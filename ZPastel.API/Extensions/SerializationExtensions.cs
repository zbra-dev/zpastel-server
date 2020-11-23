using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZPastel.API.Extensions
{
    public static class SerializationExtensions
    {
        public static async Task<T> Deserialize<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }

        public static StringContent SerializeHttpRequestData(this object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }
    }
}
