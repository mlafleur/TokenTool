using JWT.Builder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokenTool.Microsoft.AuthorizationCodeGrant
{
    public class TokenResponse
    {
        private Dictionary<string, object> _accessTokenClaims;
        private Dictionary<string, object> _idTokenClaims;

        /// <summary>
        /// The access token issued by the authorization server.
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; private set; }

        /// <summary>
        /// The lifetime in seconds of the access token.
        /// For example, the value "3600" denotes that the access token will
        /// expire in one hour from the time the response was generated.
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; private set; }

        /// <summary>
        /// The ID Token for the user. Only populated when using OpenIdConnect
        /// </summary>
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; private set; }

        [JsonIgnore]
        public Dictionary<string, object> IdTokenClaims { get; set; }

        /// <summary>
        /// The refresh token, which can be used to obtain new access tokens using the same authorization grant
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; private set; }

        /// <summary>
        /// The scope (pmerissions) provided by the access token
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; private set; }

        /// <summary>
        ///  The exact value received from the client request
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// The type of the token issued (i.e. "bearer"). Value is case insensitive.
        /// </summary>
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; private set; }

        public Dictionary<string, object> DecodeAccessToken()
        {
            if (_accessTokenClaims == null)
            {
                string json = new JwtBuilder().Decode(AccessToken);
                _accessTokenClaims = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            return _accessTokenClaims;
        }

        public Dictionary<string, object> DecodeIdToken()
        {
            if (_idTokenClaims == null) ;
            {
                string json = new JwtBuilder().Decode(IdToken);
                _idTokenClaims = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            return _idTokenClaims;
        }
    }
}