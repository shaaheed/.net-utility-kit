using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Msi.UtilityKit.Http
{
    public static class HttpUtilities
    {

        public static void SetBearerToken(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static bool IsMultipartContentType(this string contentType)
        {
            return !string.IsNullOrEmpty(contentType) && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }

    }
}
