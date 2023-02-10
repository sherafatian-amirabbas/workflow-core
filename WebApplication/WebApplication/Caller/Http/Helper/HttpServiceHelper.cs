using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Json;
using JsonNet = System.Text.Json;
using System.Linq;

using Newtonsoft.Json.Linq;

using WebApplication.Caller.Http.Helper.Interface;
using System.Globalization;

namespace WebApplication.Caller.Http.Helper
{
    public class HttpServiceHelper : IHttpServiceHelper
    {
        public HttpContent GetHttpContent(string jsonString,
            HttpFormContentType type = HttpFormContentType.FormUrlEncoded)
        {
            var o = JObject.Parse(jsonString);
            var items = o.ToObject<Dictionary<string, object>>();
            return GetHttpContent(items, type);
        }

        public HttpContent GetHttpContent(IDictionary<string, object> parameters,
            HttpFormContentType type = HttpFormContentType.FormUrlEncoded)
        {
            var builder = new HttpKeyValueContentBuilder(type);
            parameters.ForEach(u => builder.AddParam(u.Key, u.Value));
            return builder.GetHttpContent();
        }

        public HttpContent GetJsonHttpContentFromJsonString(string jsonString)
        {
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }

        public HttpContent GetJsonHttpContent(object content)
        {
            return JsonContent.Create(content);
        }

        public string GetMessageResponseAsString(HttpResponseMessage message)
        {
            return message.Content.ReadAsStringAsync().Result;
        }

        public TResponse GetResponse<TResponse>(HttpResponseMessage message)
        {
            var content = GetMessageResponseAsString(message);
            return JsonNet.JsonSerializer.Deserialize<TResponse>(content);
        }
    }
}