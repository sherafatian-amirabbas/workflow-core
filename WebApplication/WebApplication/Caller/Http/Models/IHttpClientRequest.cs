using System.Collections.Generic;
using System.Net.Http;

using WebApplication.Caller.Http.Authentication;


namespace WebApplication.Caller.Http.Models
{
    public interface IHttpClientRequest
    {
        string Url { get; }

        AuthenticationScheme AuthScheme { get; }

        List<KeyValuePair<string, string>> Headers { get; }

        HttpContent Content { get; }
    }
}
