using Newtonsoft.Json;

namespace TokenTool.MicrosoftV2
{
    public class AccessToken
    {
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}