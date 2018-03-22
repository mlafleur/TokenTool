using Newtonsoft.Json;

namespace TokenTool.Common
{
    public abstract class IDTokenBase
    {
        /// <summary>
        /// Audience(s) that this ID Token is intended for.
        /// </summary>
        [JsonProperty(PropertyName = "aud")]
        public string Audience { get; set; }

        /// <summary>
        /// Identifies the Authentication Context Class that the authentication performed satisfied.
        /// </summary>
        [JsonProperty(PropertyName = "acr")]
        public string AuthenticationClassReference { get; set; }

        /// <summary>
        /// JSON array of strings that are identifiers for authentication methods used in the authentication.
        /// </summary>
        [JsonProperty(PropertyName = "amr")]
        public string AuthenticationMethodsReferences { get; set; }

        /// <summary>
        /// Time when the End-User authentication occurred.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.
        /// </summary>
        [JsonProperty(PropertyName = "auth_time")]
        public long? AuthenticationTime { get; set; }

        /// <summary>
        ///  The party to which the ID Token was issued.
        ///  This Claim is only needed when the ID Token has a single audience value and that audience is different than the authorized party.
        /// </summary>
        [JsonProperty(PropertyName = "azp")]
        public string AuthorizedParty { get; set; }

        /// <summary>
        /// Expiration time on or after which the ID Token MUST NOT be accepted for processing.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.
        /// </summary>
        [JsonProperty(PropertyName = "exp")]
        public long? ExpirationTime { get; set; }

        /// <summary>
        /// Time at which the JWT was issued.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.
        /// </summary>
        [JsonProperty(PropertyName = "iat")]
        public long? IssuedAt { get; set; }

        /// <summary>
        /// Identifier for the Issuer of the response.
        /// Value is a case sensitive URL using the https scheme that contains scheme, host, and optionally, port number and path components and no query or fragment components.
        /// </summary>
        [JsonProperty(PropertyName = "iss")]
        public string Issuer { get; set; }

        /// <summary>
        /// String value used to associate a Client session with an ID Token, and to mitigate replay attacks.
        /// If present in the ID Token, Clients MUST verify that the nonce Claim Value is equal to the value of the nonce parameter sent in the Authentication.
        /// </summary>
        [JsonProperty(PropertyName = "nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// A locally unique and never reassigned identifier within the Issuer for the End-User, which is intended to be consumed by the Client. The value is a case sensitive string.
        /// </summary>
        [JsonProperty(PropertyName = "sub")]
        public string Subject { get; set; }
    }
}