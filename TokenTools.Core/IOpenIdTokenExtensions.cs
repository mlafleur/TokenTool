using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokenTools.Core
{
    /// <summary>
    /// Open ID Connect Extentions to JWT
    /// </summary>
    public interface IOpenIdTokenExtensions : IJsonWebToken
    {
        /// <summary>
        /// The access token hash is included in ID tokens only when the ID token is issued with an
        /// OAuth 2.0 Authorization Code Grant. It can be used to validate the authenticity of an
        /// access token. (at_hash)
        /// </summary>
        [JsonProperty(PropertyName = "at_hash")]
        string AccessTokenHash { get;  }

        /// <summary>
        /// Preferred postal address
        /// </summary>
        [JsonProperty("address")]
        string Address { get;  }

        /// <summary>
        /// Identifies the Authentication Context Class that the authentication performed satisfied. (acr)
        /// </summary>
        [JsonProperty(PropertyName = "acr")]
        string AuthenticationClassReference { get; }

        /// <summary>
        /// Array of strings that are identifiers for authentication methods used in the authentication. (amr)
        /// </summary>
        [JsonProperty(PropertyName = "amr")]
        string[] AuthenticationMethodsReferences { get; }

        /// <summary>
        /// Time when the End-User authentication occurred.
        /// Its value is a JSON number representing the number of seconds from 1970-01-01T0:0:0Z as measured in UTC until the date/time. (auth_time)
        /// </summary>
        [JsonProperty(PropertyName = "auth_time")]
        long? AuthenticationTime { get; }

        /// <summary>
        ///  The party to which the ID Token was issued.
        ///  This Claim is only needed when the ID Token has a single audience value and that audience is different than the authorized party. (azp)
        /// </summary>
        [JsonProperty(PropertyName = "azp")]
        string AuthorizedParty { get; }

        /// <summary>
        /// Birthday
        /// </summary>
        [JsonProperty("birthdate")]
        string Birthdate { get;  }

        /// <summary>
        /// The code hash is included in ID tokens only when the ID token is issued with an OAuth 2.0 authorization code. It can be used to validate the authenticity of an authorization code.
        /// </summary>
        [JsonProperty(PropertyName = "c_hash")]
        string CodeHash { get;  }

        /// <summary>
        /// Preferred e-mail address
        /// </summary>
        [JsonProperty("email")]
        string Email { get;  }

        /// <summary>
        /// True if the e-mail address has been verified; otherwise false
        /// </summary>
        [JsonProperty("email_verified")]
        bool EmailVerified { get;  }

        /// <summary>
        /// Surname or last name
        /// </summary>
        [JsonProperty(PropertyName = "family_name")]
        string FamilyName { get;  }

        /// <summary>
        /// Gender
        /// </summary>
        [JsonProperty("gender")]
        string Gender { get;  }

        /// <summary>
        /// Given name or first name (given_name)
        /// </summary>
        [JsonProperty(PropertyName = "given_name")]
        string GivenName { get;  }

        /// <summary>
        /// Locale
        /// </summary>
        [JsonProperty("locale")]
        string Locale { get;  }

        /// <summary>
        /// Middle name	(middle_name)
        /// </summary>
        [JsonProperty(PropertyName = "middle_name")]
        string MiddleName { get;  }

        /// <summary>
        /// Full Name (name)
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        string Name { get;  }

        /// <summary>
        /// Casual name	 (nickname)
        /// </summary>
        [JsonProperty(PropertyName = "nickname")]
        string Nickname { get;  }

        /// <summary>
        /// String value used to associate a Client session with an ID Token, and to mitigate replay attacks.
        /// If present in the ID Token, Clients MUST verify that the nonce Claim Value is equal to the value of the nonce parameter sent in the Authentication. (nonce)
        /// </summary>
        [JsonProperty(PropertyName = "nonce")]
        string Nonce { get; }

        /// <summary>
        /// Preferred telephone number
        /// </summary>
        [JsonProperty("phone_number")]
        string PhoneNumber { get;  }

        /// <summary>
        /// True if the phone number has been verified; otherwise false
        /// </summary>
        [JsonProperty("phone_number_verified")]
        bool PhoneNumberVerified { get;  }

        /// <summary>
        /// Profile picture URL
        /// </summary>
        [JsonProperty("picture")]
        string Picture { get;  }

        /// <summary>
        /// Shorthand name by which the End-User wishes to be referred to
        /// </summary>
        [JsonProperty("preferred_username")]
        string PreferredUsername { get;  }

        /// <summary>
        /// Profile page URL
        /// </summary>
        [JsonProperty("profile")]
        string Profile { get;  }

        /// <summary>
        /// Public key used to check the signature of an ID Token
        /// </summary>
        [JsonProperty(PropertyName = "sub_jwk")]
        string SubJwk { get;  }

        /// <summary>
        /// Time the information was last updated
        /// </summary>
        [JsonProperty("updated_at")]
        string UpdatedAt { get;  }

        /// <summary>
        /// Web page or blog URL
        /// </summary>
        [JsonProperty("website")]
        string Website { get;  }

        /// <summary>
        /// Time zone
        /// </summary>
        [JsonProperty("zoneinfo")]
        string ZoneInfo { get;  }
    }
}