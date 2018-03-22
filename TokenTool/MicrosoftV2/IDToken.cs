using JWT.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TokenTool.MicrosoftV2
{
    public class IDToken : Common.IDTokenBase
    {
        /// <summary>
        /// The access token hash is included in ID tokens only when the ID token is issued with an OAuth 2.0 access token. It can be used to validate the authenticity of an access token.
        /// </summary>
        [JsonProperty(PropertyName = "at_hash")]
        public string AccessTokenHash { get; set; }

        /// <summary>
        /// The code hash is included in ID tokens only when the ID token is issued with an OAuth 2.0 authorization code. It can be used to validate the authenticity of an authorization code.
        /// </summary>
        [JsonProperty(PropertyName = "c_hash")]
        public string CodeHash { get; set; }

        /// <summary>
        /// The name claim provides a human-readable value that identifies the subject of the token. The value is not guaranteed to be unique, it is mutable, and it's designed to be used only for display purposes.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// The name claim provides a human-readable value that identifies the subject of the token. The value is not guaranteed to be unique, it is mutable, and it's designed to be used only for display purposes.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The time at which the token becomes valid, represented in epoch time. It is usually the same as the issuance time. Your app should use this claim to verify the validity of the token lifetime.
        /// </summary>
        [JsonProperty(PropertyName = "nbf")]
        public long? NotBefore { get; set; }

        /// <summary>
        /// The immutable identifier for an object in the Microsoft identity system, in this case, a user account. It can also be used to perform authorization checks safely and as a key in database tables.
        /// </summary>
        [JsonProperty(PropertyName = "oid")]
        public string ObjectID { get; set; }

        /// <summary>
        /// The primary username that represents the user in the v2.0 endpoint. It could be an email address, phone number, or a generic username without a specified format. Its value is mutable and might change over time. Since it is mutable, this value must not be used to make authorization decisions.
        /// </summary>
        [JsonProperty(PropertyName = "preferred_username")]
        public string PreferredUsername { get; set; }

        /// <summary>
        /// A GUID that represents the Azure AD tenant that the user is from.
        /// </summary>
        [JsonProperty(PropertyName = "tid")]
        public string TenantID { get; set; }

        /// <summary>
        /// The version of the ID token, as defined by Azure AD
        /// </summary>
        [JsonProperty(PropertyName = "ver")]
        public string Version { get; set; }

        public static IDToken Parse(string idToken)
        {
            string json = new JwtBuilder().Decode(idToken);

            var raw = JObject.Parse(json);
            var typed = JObject.Parse(json).ToObject<IDToken>();

            return typed;
        }
    }
}