using System.Net.Http;
using System.Threading.Tasks;

using WebApplication.Caller.Http.Models;


namespace WebApplication.Caller.Http.Service
{
    public sealed class CallerHttpClient
    {
        private readonly HttpClient client;


        #region Constructors

        public CallerHttpClient(HttpClient client)
        {
            this.client = @client;
        }

        #endregion


        #region Public Methods

        public Task<HttpResponseMessage> SendAsync(HttpMethod method, IHttpClientRequest param)
        {
            var httpMessage = new HttpRequestMessage(method, param.Url);

            if (param.Content != null)
                httpMessage.Content = param.Content;

            AppendHeadersToTheMessage(httpMessage, param);

            return client.SendAsync(httpMessage);
        }

        private void AppendHeadersToTheMessage(HttpRequestMessage httpMessage, IHttpClientRequest param)
        {
            if (param.AuthScheme != null)
                httpMessage.Headers.Authorization = param.AuthScheme.GetAuthenticationHeaderValue();

            param.Headers?.ForEach(header => httpMessage.Headers.Add(header.Key, header.Value));
        }

        #endregion
    }
}
