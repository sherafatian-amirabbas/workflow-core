using System;
using System.Collections.Generic;
using System.Net.Http;


namespace WebApplication.Caller.Http.Helper
{
    public class HttpKeyValueContentBuilder
    {
        private readonly HttpFormContentType type;
        private readonly List<KeyValuePair<string, string>> data;


        #region Constructors

        public HttpKeyValueContentBuilder(HttpFormContentType type)
        {
            data = new List<KeyValuePair<string, string>>();
            this.type = type;
        }

        #endregion


        #region Public Methods

        public HttpKeyValueContentBuilder AddParam(string key, object value)
        {
            var val = value == null ? string.Empty : value.ToString();
            data.Add(new KeyValuePair<string, string>(key, val));
            return this;
        }

        public HttpContent GetHttpContent()
        {
            switch (type)
            {
                case HttpFormContentType.FormUrlEncoded:
                    return new FormUrlEncodedContent(data);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}
