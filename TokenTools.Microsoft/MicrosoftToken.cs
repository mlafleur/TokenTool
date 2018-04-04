using JWT.Builder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using TokenTools.Core;

namespace TokenTools.Microsoft
{
    /// <summary>
    /// Microsoft Token
    /// Used for id_tokens and as a base class for access_tokens returned by the Microsoft v2 Endpoint
    /// </summary>
    public class MicrosoftToken : IJsonWebToken, IOpenIdTokenExtensions, IMicrosoftTokenExtensions
    {
        public string AccessTokenHash { get; }
        public string Address { get; }
        public string Audience { get; }
        public string AuthenticationClassReference { get; }
        public string[] AuthenticationMethodsReferences { get; }
        public long? AuthenticationTime { get; }
        public string AuthorizedParty { get; }
        public string Birthdate { get; }
        public string CodeHash { get; }
        public string Email { get; }
        public bool EmailVerified { get; }
        public long? ExpirationTime { get; }
        public string FamilyName { get; }
        public string Gender { get; }
        public string GivenName { get; }
        public long? IssuedAt { get; }
        public string Issuer { get; }
        public long? JwtId { get; }
        public string Locale { get; }
        public string MiddleName { get; }
        public string Name { get; }
        public string Nickname { get; }
        public string Nonce { get; }
        public long? NotBefore { get; set; }
        public string ObjectID { get; }
        public string PhoneNumber { get; }
        public bool PhoneNumberVerified { get; }
        public string Picture { get; }
        public string PreferredUsername { get; }
        public string Profile { get; }
        public string Subject { get; }
        public string SubJwk { get; }
        public string TenantID { get; }
        public string UpdatedAt { get; }
        public string Version { get; }
        public string Website { get; }
        public string ZoneInfo { get; }

        /// <summary>
        /// Parse a JWT into a new MicrosoftToken object
        /// </summary>
        public static MicrosoftToken Parse(string jsonWebToken)
        {
            string json = new JwtBuilder().Decode(jsonWebToken);

            var raw = JObject.Parse(json);
            var typed = JObject.Parse(json).ToObject<MicrosoftToken>();

            return typed;
        }
    }
}