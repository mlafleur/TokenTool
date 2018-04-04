using JWT.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using TokenTools.Core;

namespace TokenTools.Microsoft

{
    /// <summary>
    /// A Microsoft v2 Endpoint Access Token.
    /// Based on the JSON Web Token spec with OpenID Connect Token and Azure AD v2 Endpoint Extentions
    /// </summary>
    public class MicrosoftAccessToken : MicrosoftToken
    {
        /// <summary>
        /// Identifies the application that is using the token to access a resource.
        /// The application can act as itself or on behalf of a user.
        /// The application ID typically represents an application object, but it can also represent a service principal object in Azure AD.
        /// </summary>
        [JsonProperty(PropertyName = "appid")]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Indicates how the client was authenticated. For a public client, the value is 0. If client ID and client secret are used, the value is 1.
        /// </summary>
        [JsonProperty(PropertyName = "appidacr")]
        public string ApplicationIdAcr { get; set; }

        [JsonProperty(PropertyName = "deviceid")]
        public string DeviceId { get; set; }

        /// <summary>
        /// IP Address of the device that requested the token
        /// </summary>
        [JsonProperty(PropertyName = "idaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Indicates the impersonation permissions granted to the client application. The default permission is user_impersonation. The owner of the secured resource can register additional values in Azure AD.
        /// </summary>
        [JsonProperty(PropertyName = "scp")]
        public string Scopes { get; set; }

        /// <summary>
        /// Provides a human readable value that identifies the subject of the token. This value is not guaranteed to be unique within a tenant and is designed to be used only for display purposes.
        /// </summary>
        [JsonProperty(PropertyName = "unique_name")]
        public string UniqueName { get; set; }

        /// <summary>
        /// Stores the user name of the user principal.
        /// </summary>
        [JsonProperty(PropertyName = "upn")]
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// Parse a JWT "access_token" into a new MicrosoftAccessToken object
        /// </summary>
        public new static MicrosoftAccessToken Parse(string access_token)
        {
            string json = new JwtBuilder().Decode(access_token);

            var raw = JObject.Parse(json);
            var typed = JObject.Parse(json).ToObject<MicrosoftAccessToken>();

            return typed;
        }
    }
}