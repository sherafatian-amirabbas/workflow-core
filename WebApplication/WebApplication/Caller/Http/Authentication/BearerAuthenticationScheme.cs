using System.Net.Http.Headers;


namespace WebApplication.Caller.Http.Authentication
{
    public class BearerAuthenticationScheme : AuthenticationScheme
    {
        public BearerAuthenticationScheme(string data)
            : base(HttpAuthenticationSchemeType.Bearer)
        {
            this.Data = data;
        }


        #region AuthenticationSchemeBase

        public override string Data { get; }

        AuthenticationHeaderValue value = null;
        public override AuthenticationHeaderValue GetAuthenticationHeaderValue()
        {
            if (value == null)
                value = new AuthenticationHeaderValue("Bearer", this.Data);

            return value;
        }

        #endregion
    }
}
