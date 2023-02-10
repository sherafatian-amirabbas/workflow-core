namespace WebApplication.Caller.Http
{
    /// <summary>
    /// A complete scheme list can be found at:
    /// https://www.iana.org/assignments/http-authschemes/http-authschemes.xhtml
    /// </summary>
    public enum HttpAuthenticationSchemeType
    {
        Basic,
        Bearer
    }

    public enum HttpFormContentType
    {
        FormUrlEncoded
    }

    public enum HttpCallerMethod
    {
        GET,
        PUT,
        POST,
        DELETE,
        PATCH
    }
}
