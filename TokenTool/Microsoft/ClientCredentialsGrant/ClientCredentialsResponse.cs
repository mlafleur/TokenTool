using JWT.Builder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokenTool.Microsoft.ClientCredentialsGrant
{
    public class ClientCredentialsResponse
    {
        private Dictionary<string, object> _accessTokenClaims;

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
    }
}