using System.IO.Compression;
using System.Text.Json;

namespace Magnus.Futbot.Selenium.Trading.Connections
{
    public abstract class PutConnection<T> : BaseConnection
    {
        protected PutConnection(HttpClient httpClient) : base(httpClient)
        {
        }

        protected async Task<string> PutAsync(T content)
        {
            var response = await _httpClient.PutAsync(BaseAddress, new StringContent(JsonSerializer.Serialize(content)));
            using Stream stream = await response.Content.ReadAsStreamAsync();
            using Stream decompressed = new GZipStream(stream, CompressionMode.Decompress);
            using StreamReader reader = new(decompressed);
            return reader.ReadToEnd();
        }
    }
}
