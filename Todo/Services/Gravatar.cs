using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services
{
     public static class Gravatar
    {
        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static async Task<string> ProfileName(string emailAddress)
        {
            using HttpClient client = new();
            try
            {
                var json = await client.GetStringAsync($"https://en.gravatar.com/{GetHash(emailAddress)}.json");
                JToken token = JObject.Parse(json);
                string displayName = (string)token.SelectToken("entry")[0].SelectToken("displayName");

                return displayName;
            }
            catch(HttpRequestException)
            {
                return String.Empty;
            }
        }
    }
}