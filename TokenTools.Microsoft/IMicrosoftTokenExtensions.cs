using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TokenTools.Core;

namespace TokenTools.Microsoft
{
    /// <summary>
    /// Microsoft v2 Endpoint Extensions to the  JWT/OpenID Token Specifications
    /// </summary>
    public interface IMicrosoftTokenExtensions : IJsonWebToken, IOpenIdTokenExtensions
    {
        /// <summary>
        /// The immutable identifier for an object in the Microsoft identity system, in this case, a user account. It can also be used to perform authorization checks safely and as a key in database tables.
        /// </summary>
        [JsonProperty(PropertyName = "oid")]
        string ObjectID { get; }

        /// <summary>
        /// A GUID that represents the Azure AD tenant that the user is from.
        /// </summary>
        [JsonProperty(PropertyName = "tid")]
        string TenantID { get; }

        /// <summary>
        /// The version of the ID token, as defined by Azure AD
        /// </summary>
        [JsonProperty(PropertyName = "ver")]
        string Version { get; }
    }
}