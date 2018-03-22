using Newtonsoft.Json;

namespace TokenTool.Common
{
    public abstract class OAuthTokenBase
    {
        /// <summary>
        /// The access token issued by the authorization server.
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// The type of the token issued (i.e. "bearer"). Value is case insensitive.
        /// </summary>
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// The lifetime in seconds of the access token.
        /// For example, the value "3600" denotes that the access token will
        /// expire in one hour from the time the response was generated.
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }

        /// <summary>
        /// The refresh token, which can be used to obtain new access tokens using the same authorization grant
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The scope (pmerissions) provided by the access token
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        /// <summary>
        ///  The exact value received from the client request
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// The ID Token for the user. Only populated when using OpenIdConnect
        /// </summary>
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }
    }
}