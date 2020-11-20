using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ZPastel.Test.Utils
{
    static class SerializeUtils
    {
        public static StringContent Serialize(object value)
        {
            return new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        }
    }
}
