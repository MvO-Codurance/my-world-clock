using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;

namespace Microsoft.Azure.Functions.Isolated.TestDoubles.Extensions
{
    public static class HttpResponseDataExtensions
    {
        public static string ReadHttpResponseData(this HttpResponseData response)
        {
            var stream = response.Body;
            if (stream is MemoryStream)
            {
                if (stream.Position != 0)
                {
                    stream.Position = 0;
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            return string.Empty;
        }
        
        public static T? ReadHttpResponseDataAsJson<T>(this HttpResponseData response)
        {
            var stream = response.Body;
            if (stream is MemoryStream)
            {
                if (stream.Position != 0)
                {
                    stream.Position = 0;
                }

                return JsonSerializer.Deserialize<T>(stream);
            }

            return default;
        }
        
        public static T? ReadHttpResponseDataAsJson<T>(
            this HttpResponseData response,
            JsonSerializerOptions jsonSerializerOptions)
        {
            var stream = response.Body;
            if (stream is MemoryStream)
            {
                if (stream.Position != 0)
                {
                    stream.Position = 0;
                }

                return JsonSerializer.Deserialize<T>(stream, jsonSerializerOptions);
            }

            return default;
        }

        public static bool IsHttpResponseBodyEmpty(this HttpResponseData response)
        {
            return response.Body.IsEmpty();
        }
    }
}
