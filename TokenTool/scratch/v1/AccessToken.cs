using Newtonsoft.Json;

namespace TokenTool.Microsoft.v1
{
    public class AccessToken : Core.OAuthTokenBase
    {
        [JsonProperty(PropertyName = "expires_on")]
        public int ExpiresOn { get; set; }

        [JsonProperty(PropertyName = "resource")]
        public string Resource { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }
    }
}