using System.Collections.Generic;
using System.Net.Http;


namespace WebApplication.Caller.Http.Helper.Interface
{
    public interface IHttpServiceHelper
    {
        HttpContent GetHttpContent(string jsonString,
            HttpFormContentType type = HttpFormContentType.FormUrlEncoded);

        HttpContent GetHttpContent(IDictionary<string, object> parameters,
            HttpFormContentType type = HttpFormContentType.FormUrlEncoded);

        HttpContent GetJsonHttpContentFromJsonString(string jsonString);
        HttpContent GetJsonHttpContent(object content);

        string GetMessageResponseAsString(HttpResponseMessage message);

        TResponse GetResponse<TResponse>(HttpResponseMessage message);
    }
}