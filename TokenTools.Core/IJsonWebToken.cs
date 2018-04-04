using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokenTools.Core
{
    public interface IJsonWebToken
    {
        /// <summary>
        /// Audience(s) that this ID Token is intended for. (aud)
        /// </summary>
        [JsonProperty(PropertyName = "aud")]
        string Audience { get; }

        /// <summary>
        /// Expiration time on or after which the ID Token MUST NOT be accepted for processing.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time. (exp)
        /// </summary>
        [JsonProperty(PropertyName = "exp")]
        long? ExpirationTime { get; }

        /// <summary>
        /// Time at which the JWT was issued.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time. (iat)
        /// </summary>
        [JsonProperty(PropertyName = "iat")]
        long? IssuedAt { get; }

        /// <summary>
        /// Identifier for the Issuer of the response.
        /// Value is a case sensitive URL using the https scheme that contains scheme, host, and optionally, port number and path components and no query or fragment components. (iss)
        /// </summary>
        [JsonProperty(PropertyName = "iss")]
        string Issuer { get; }

        /// <summary>
        /// Time at which the JWT was issued.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time. (iat)
        /// </summary>
        [JsonProperty(PropertyName = "jti")]
        long? JwtId { get; }

        /// <summary>
        /// The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
        /// </summary>
        [JsonProperty(PropertyName = "nbf")]
        long? NotBefore { get; set; }

        /// <summary>
        /// A locally unique and never reassigned identifier within the Issuer for the End-User, which is intended to be consumed by the Client. The value is a case sensitive string. (sub)
        /// </summary>
        [JsonProperty(PropertyName = "sub")]
        string Subject { get; }
    }
}