using JWT.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokenTool.Microsoft
{
    public class AccessToken
    {
        //public string aio { get; set; }

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

        /// <summary>
        /// The intended recipient of the token.
        /// </summary>
        [JsonProperty(PropertyName = "aud")]
        public string Audience { get; set; }

        /// <summary>
        /// Indicates how the subject was authenticated, as opposed to the client in the Application Authentication Context Class Reference claim.
        /// A value of "0" indicates the end-user authentication did not meet the requirements of ISO/IEC 29115.
        /// </summary>
        [JsonProperty(PropertyName = "acr")]
        public string AuthenticationClassReference { get; set; }

        /// <summary>
        /// Identifies how the subject of the token was authenticated.
        /// </summary>
        [JsonProperty(PropertyName = "amr")]
        public List<string> AuthenticationMethodsReferences { get; set; }

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
        /// The code hash is included in ID tokens only when the ID token is issued with an OAuth 2.0 authorization code. It can be used to validate the authenticity of an authorization code.
        /// </summary>
        [JsonProperty(PropertyName = "c_hash")]
        public string CodeHash { get; set; }

        [JsonProperty(PropertyName = "deviceid")]
        public string DeviceId { get; set; }

        /// <summary>
        /// The name claim provides a human-readable value that identifies the subject of the token. The value is not guaranteed to be unique, it is mutable, and it's designed to be used only for display purposes.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// The "exp" (expiration time) claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.
        /// </summary>
        [JsonProperty(PropertyName = "exp")]
        public long? ExpirationTime { get; set; }

        /// <summary>
        /// Provides the last name, surname, or family name of the user as defined in the Azure AD user object.
        /// </summary>
        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

        /// <summary>
        /// Provides the first or given name of the user, as set on the Azure AD user object.
        /// </summary>
        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

        /// <summary>
        /// IP Address of the device that requested the token
        /// </summary>
        [JsonProperty(PropertyName = "idaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Stores the time at which the token was issued. It is often used to measure token freshness.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time.
        /// </summary>
        [JsonProperty(PropertyName = "iat")]
        public long? IssuedAt { get; set; }

        /// <summary>
        /// Identifies the security token service (STS) that constructs and returns the token.
        /// In the tokens that Azure AD returns, the issuer is sts.windows.net.
        /// The GUID in the Issuer claim value is the tenant ID of the Azure AD directory.
        /// The tenant ID is an immutable and reliable identifier of the directory.
        /// </summary>
        [JsonProperty(PropertyName = "iss")]
        public string Issuer { get; set; }

        /// <summary>
        /// The name claim provides a human-readable value that identifies the subject of the token. The value is not guaranteed to be unique, it is mutable, and it's designed to be used only for display purposes.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// String value used to associate a Client session with an ID Token, and to mitigate replay attacks.
        /// If present in the ID Token, Clients MUST verify that the nonce Claim Value is equal to the value of the nonce parameter sent in the Authentication.
        /// </summary>
        [JsonProperty(PropertyName = "nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
        /// </summary>
        [JsonProperty(PropertyName = "nbf")]
        public long? NotBefore { get; set; }

        /// <summary>
        /// Contains a unique identifier of an object in Azure AD. This value is immutable and cannot be reassigned or reused. Use the object ID to identify an object in queries to Azure AD.
        /// </summary>
        [JsonProperty(PropertyName = "oid")]
        public string ObjectID { get; set; }

        //public string onprem_sid { get; set; }

        //public string platf { get; set; }

        /// <summary>
        /// The primary username that represents the user in the v2.0 endpoint. It could be an email address, phone number, or a generic username without a specified format. Its value is mutable and might change over time. Since it is mutable, this value must not be used to make authorization decisions.
        /// </summary>
        [JsonProperty(PropertyName = "preferred_username")]
        public string PreferredUsername { get; set; }

        //public string puid { get; set; }

        /// <summary>
        /// Indicates the impersonation permissions granted to the client application. The default permission is user_impersonation. The owner of the secured resource can register additional values in Azure AD.
        /// </summary>
        [JsonProperty(PropertyName = "scp")]
        public string Scopes { get; set; }

        /// <summary>
        /// Identifies the principal about which the token asserts information, such as the user of an application. This value is immutable and cannot be reassigned or reused, so it can be used to perform authorization checks safely. Because the subject is always present in the tokens the Azure AD issues, we recommended using this value in a general purpose authorization system.
        /// </summary>
        [JsonProperty(PropertyName = "sub")]
        public string Subject { get; set; }

        /// <summary>
        /// An immutable, non-reusable identifier that identifies the directory tenant that issued the token. You can use this value to access tenant-specific directory resources in a multi-tenant application. For example, you can use this value to identify the tenant in a call to the Graph API.
        /// </summary>
        [JsonProperty(PropertyName = "tid")]
        public string TenantID { get; set; }

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

        //public string uti { get; set; }

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