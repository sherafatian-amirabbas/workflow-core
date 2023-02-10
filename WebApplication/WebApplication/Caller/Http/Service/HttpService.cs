using System;
using System.Net.Http;

using WebApplication.Caller.Http.Helper.Interface;
using WebApplication.Caller.Http.Models;
using WebApplication.Caller.Http.Service.Interface;


namespace WebApplication.Caller.Http.Service
{
    public sealed class HttpService : IHttpService
    {
        private readonly CallerHttpClient client;
        private IHttpServiceHelper serviceHelper;


        #region Constructors

        public HttpService(CallerHttpClient client,
            IHttpServiceHelper serviceHelper)
        {
            this.client = @client;
            this.serviceHelper = @serviceHelper;
        }

        #endregion


        #region Public Methods

        public HttpResponseMessage Get(IHttpClientRequest param)
        {
            return Invoke(HttpCallerMethod.GET, param);
        }

        public TResponse Get<TResponse>(IHttpClientRequest param)
        {
            return Invoke<TResponse>(HttpCallerMethod.GET, param);
        }


        public HttpResponseMessage Put(IHttpClientRequest param)
        {
            return Invoke(HttpCallerMethod.PUT, param);
        }

        public TResponse Put<TResponse>(IHttpClientRequest param)
        {
            return Invoke<TResponse>(HttpCallerMethod.PUT, param);
        }


        public HttpResponseMessage Post(IHttpClientRequest param)
        {
            return Invoke(HttpCallerMethod.POST, param);
        }

        public TResponse Post<TResponse>(IHttpClientRequest param)
        {
            return Invoke<TResponse>(HttpCallerMethod.POST, param);
        }


        public HttpResponseMessage Delete(IHttpClientRequest param)
        {
            return Invoke(HttpCallerMethod.DELETE, param);
        }

        public TResponse Delete<TResponse>(IHttpClientRequest param)
        {
            return Invoke<TResponse>(HttpCallerMethod.DELETE, param);
        }


        public HttpResponseMessage Patch(IHttpClientRequest param)
        {
            return Invoke(HttpCallerMethod.PATCH, param);
        }

        public TResponse Patch<TResponse>(IHttpClientRequest param)
        {
            return Invoke<TResponse>(HttpCallerMethod.PATCH, param);
        }


        public HttpResponseMessage Invoke(HttpCallerMethod method, IHttpClientRequest param)
        {
            var httpMethod = GetHttpMethod(method);
            return Invoke(httpMethod, param);
        }

        public TResponse Invoke<TResponse>(HttpCallerMethod method, IHttpClientRequest param)
        {
            var response = Invoke(method, param);
            return serviceHelper.GetResponse<TResponse>(response);
        }


        public HttpResponseMessage Invoke(HttpMethod method, IHttpClientRequest param)
        {
            return this.client.SendAsync(method, param).Result;
        }

        public TResponse Invoke<TResponse>(HttpMethod method, IHttpClientRequest param)
        {
            var response = Invoke(method, param);
            return serviceHelper.GetResponse<TResponse>(response);
        }

        #endregion


        #region Private Methods

        private HttpMethod GetHttpMethod(HttpCallerMethod method)
        {
            switch (method)
            {
                case HttpCallerMethod.GET:
                    return HttpMethod.Get;
                case HttpCallerMethod.PUT:
                    return HttpMethod.Put;
                case HttpCallerMethod.POST:
                    return HttpMethod.Post;
                case HttpCallerMethod.DELETE:
                    return HttpMethod.Delete;
                case HttpCallerMethod.PATCH:
                    return HttpMethod.Patch;
                default:
                    throw new NotImplementedException("HttpMethod is not defined for HttpCallerMethod");
            }
        }

        #endregion
    }
}
