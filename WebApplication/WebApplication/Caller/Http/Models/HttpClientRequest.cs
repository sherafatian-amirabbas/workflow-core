using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

using WebApplication.Caller.Http.Authentication;


namespace WebApplication.Caller.Http.Models
{
    public class HttpClientRequest : IHttpClientRequest
    {
        public HttpClientRequest(string url,
            AuthenticationScheme authScheme,
            List<KeyValuePair<string, string>> headers,
            HttpContent content)
        {
            Url = url;
            AuthScheme = authScheme;
            Headers = headers;
            Content = content;
        }

        #region Properties

        public string Url { get; }

        public AuthenticationScheme AuthScheme { get; }

        public List<KeyValuePair<string, string>> Headers { get; }

        public HttpContent Content { get; }

        #endregion


        #region Private Methods

        public override string ToString()
        {
            var objList = new List<string>()
            {
                $"Url: {this.Url}"
            };

            if (this.AuthScheme != null)
                objList.Add($"AuthenticationScheme: {this.AuthScheme}");

            if (this.Headers != null && this.Headers.Any())
            {
                var strHeaders = string.Join("\n", this.Headers.Select(u => u.ToString()));
                var strHeader = "Headers:\n{\n";
                strHeader += $"{strHeaders}\n";
                strHeader += "}";
                objList.Add(strHeader);
            }

            if (this.Content != null)
            {
                var contentStr = this.Content.ReadAsStringAsync().Result;
                objList.Add($"Content: {contentStr}");
            }

            return string.Join("\n", objList);
        }

        #endregion
    }
}