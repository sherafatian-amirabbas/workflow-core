using System.Net.Http;

using WebApplication.Caller.Http.Models;


namespace WebApplication.Caller.Http.Service.Interface
{
    public interface IHttpService
    {
        HttpResponseMessage Get(IHttpClientRequest param);
        TResponse Get<TResponse>(IHttpClientRequest param);

        HttpResponseMessage Put(IHttpClientRequest param);
        TResponse Put<TResponse>(IHttpClientRequest param);

        HttpResponseMessage Post(IHttpClientRequest param);
        TResponse Post<TResponse>(IHttpClientRequest param);

        HttpResponseMessage Delete(IHttpClientRequest param);
        TResponse Delete<TResponse>(IHttpClientRequest param);

        HttpResponseMessage Patch(IHttpClientRequest param);
        TResponse Patch<TResponse>(IHttpClientRequest param);

        HttpResponseMessage Invoke(HttpCallerMethod method, IHttpClientRequest param);
        TResponse Invoke<TResponse>(HttpCallerMethod method, IHttpClientRequest param);

        HttpResponseMessage Invoke(HttpMethod method, IHttpClientRequest param);
        TResponse Invoke<TResponse>(HttpMethod method, IHttpClientRequest param);
    }
}
