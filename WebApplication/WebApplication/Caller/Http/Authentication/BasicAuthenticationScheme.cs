using System;
using System.Net.Http.Headers;
using System.Text;


namespace WebApplication.Caller.Http.Authentication
{
    public class BasicAuthenticationScheme : AuthenticationScheme
    {
        private readonly string userId;
        private readonly string pass;


        #region Constructors

        public BasicAuthenticationScheme(string userId, string pass)
            : base(HttpAuthenticationSchemeType.Basic)
        {
            this.userId = userId;
            this.pass = pass;
        }

        #endregion


        #region AuthenticationSchemeBase

        string data;
        public override string Data
        {
            get
            {
                if (data == null)
                    this.data = GetBasicAuthCredential(this.userId, this.pass);
                return this.data;
            }
        }

        AuthenticationHeaderValue value = null;
        public override AuthenticationHeaderValue GetAuthenticationHeaderValue()
        {
            if (value == null)
                value = new AuthenticationHeaderValue("Basic", this.Data);

            return value;
        }

        #endregion


        #region Private Methods

        private string GetBasicAuthCredential(string userId, string pass)
        {
            var credentialPair = Encoding.ASCII.GetBytes($"{@userId}:{@pass}");
            var base64Credential = Convert.ToBase64String(credentialPair);
            return base64Credential;
        }

        #endregion
    }
}
