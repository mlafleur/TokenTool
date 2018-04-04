using System;
using System.Collections.Generic;
using System.Text;
using TokenTools.Core;

namespace TokenTools.Microsoft.AuthorizationCodeGrant
{
    public class AuthorizationRequest
    {
        public AuthorizationRequest(ApplicationRegistration applicationRegistration)
        {
            this.ClientId = applicationRegistration.ClientId;
            this.RedirectUri = applicationRegistration.RedirectUri;
            this.Scope = applicationRegistration.Scopes;
        }

        /// <summary>
        /// Creates an AuthorizationRequest instance for generating an Authorization URI
        /// </summary>
        /// <param name="clientId">The Application D that the registration portal (apps.dev.microsoft.com) assigned your app.</param>
        public AuthorizationRequest(string clientId, string redirectUri)
        {
            this.ClientId = clientId;
            this.RedirectUri = redirectUri;
        }

        /// <summary>
        /// The Application D that the registration portal (apps.dev.microsoft.com) assigned your app.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Used to secure authorization code grants via Proof Key for Code Exchange (PKCE) from a native client.
        /// Required if code_challenge_method is included.
        /// </summary>
        public string CodeChallenge { get; set; }

        /// <summary>
        /// The method used to encode the code_verifier for the code_challenge parameter.
        /// Azure AAD v2.0 supports both plain and S256.
        /// </summary>
        public string CodeChallengeMethod { get; set; }

        /// <summary>
        /// Can be one of consumers or organizations.
        /// If included, it will skip the email-based discovery process that user goes through on the v2.0 sign-in page,
        /// leading to a slightly more streamlined user experience.
        /// </summary>
        public string DomainHint { get; set; }

        /// <summary>
        /// Can be used to pre-fill the username/email address field of the sign-in page for the user, if you know their username ahead of time.
        /// </summary>
        public string LoginHint { get; set; }

        /// <summary>
        /// Indicates the type of user interaction that is required. The only valid values at this time are 'login', 'none', and 'consent'.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// The redirect_uri of your app, where authentication responses can be sent and received by your app.
        /// It must exactly match one of the redirect_uris you registered in the portal.
        /// For native & mobile apps, you should use the default value of "https://login.microsoftonline.com/common/oauth2/nativeclient".
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Specifies the method that should be used to send the resulting token back to your app. Can be query or form_post.
        /// </summary>
        public string ResponseMode { get; set; } = "query";

        /// <summary>
        /// Must include code for the authorization code flow.
        /// </summary>
        //public string ResponseType { get; set; } = "code";

        /// <summary>
        /// A space-separated list of scopes that you want the user to consent to.
        /// The default value is "https://graph.microsoft.com/.default" which uses the scopes provided during application registration.
        /// </summary>
        public string Scope { get; set; } = "https://graph.microsoft.com/.default";

        /// <summary>
        /// A value included in the request that will also be returned in the token response.
        /// It can be a string of any content that you wish. A randomly generated unique value is typically used for preventing cross-site request forgery attacks.
        /// The value can also encode information about the user's state in the app before the authentication request occurred, such as the page or view they were on.
        /// </summary>
        public string State { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// The Tenant property can be used to control who can sign into the application.
        /// The allowed values are "common", "organizations", "consumers", or a single Tenant's GUID
        /// </summary>
        public string Tenant { get; set; } = "common";

        /// <summary>
        /// Generates the URI to redirect a user to when launching the Authorization Code Grant authentication flow.
        /// </summary>
        /// <returns>Authroization Code Authentication URI</returns>
        public System.Uri GenerateAuthorizationUri()
        {
            /// Construct a Query String
            var queryParams = new Utils.QueryParameterCollection
            {
                { "client_id", ClientId },
                { "response_type", "code" },
                { "scope", Scope },
                { "redirect_uri", RedirectUri },
                { "response_mode", ResponseMode },
                { "state", State },
                { "prompt", Prompt},
                { "login_hint", LoginHint },
                { "domain_hint", DomainHint },
                { "code_challenge_method", CodeChallengeMethod },
                { "code_challenge", CodeChallenge }
            };

            // Define required query params
            var requiredParams = new List<string>() {
                "client_id",
                "response_type",
                "scope"
            };

            // If we have a code_challenge_method we need a code_challenge
            if (CodeChallengeMethod != null)
            {
                requiredParams.Add("code_challenge_method");
                requiredParams.Add("code_challenge");
            }

            // Validate required values are included
            if (!queryParams.ValidateKeys(requiredParams))
            {
                throw new MissingValueException($"One or more required parameters are missing or empty: {string.Join(",", requiredParams.ToArray())}");
            }

            // Ensure we have a valid ResponseMode
            if (!string.IsNullOrEmpty(Prompt) &&
                Prompt != "consent" &&
                Prompt != "login" &&
                Prompt != "none")
            {
                throw new InvalidValueException($"Valid values for ResponseMode are 'login', 'none', and 'consent'. Current value: {Prompt}");
            }

            return new Uri($"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/authorize?{queryParams.ToQueryString()}");
        }
    }
}