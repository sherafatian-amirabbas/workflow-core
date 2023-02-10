using System.Net.Http.Headers;


namespace WebApplication.Caller.Http.Authentication
{
    public abstract class AuthenticationScheme
    {
        protected readonly HttpAuthenticationSchemeType type;


        #region Constructors

        protected AuthenticationScheme(HttpAuthenticationSchemeType type)
        {
            this.type = type;
        }

        #endregion


        #region abstracts

        public abstract string Data { get; }

        public abstract AuthenticationHeaderValue GetAuthenticationHeaderValue();

        public override string ToString()
        {
            var value = this.GetAuthenticationHeaderValue();
            return value.ToString();
        }

        #endregion
    }
}
