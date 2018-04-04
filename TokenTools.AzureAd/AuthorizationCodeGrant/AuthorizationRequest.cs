using System;
using System.Collections.Generic;
using System.Text;
using TokenTools.Core;

namespace TokenTools.AzureAd.AuthorizationCodeGrant
{
    public class AuthorizationRequest
    {
        public AuthorizationRequest(ApplicationRegistration applicationRegistration)
        {
            this.ClientId = applicationRegistration.ClientId;
            this.RedirectUri = applicationRegistration.RedirectUri;
        }

        public AuthorizationRequest(string clientId, string redirectUri)
        {
            this.ClientId = clientId;
            this.RedirectUri = redirectUri;
        }

        public string ClientId { get; set; }
        public string DomainHint { get; set; }
        public string LoginHint { get; set; }
        public string Prompt { get; set; }
        public string RedirectUri { get; set; }
        public string Resource { get; set; }
        public string ResponseMode { get; set; } = "query";
        public string State { get; set; }
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
                { "resource", Resource },
                { "redirect_uri", RedirectUri },
                { "response_mode", ResponseMode },
                { "state", State },
                { "prompt", Prompt},
                { "login_hint", LoginHint },
                { "domain_hint", DomainHint }
            };

            // Define required query params
            var requiredParams = new List<string>() {
                "client_id",
                "response_type",
                "redirect_uri"
            };

            // Validate required values are included
            if (!queryParams.ValidateKeys(requiredParams))
            {
                throw new MissingValueException($"One or more required parameters are missing or empty: {string.Join(",", requiredParams.ToArray())}");
            }

            return new Uri($"https://login.microsoftonline.com/{Tenant}/oauth2/authorize?{queryParams.ToQueryString()}");
        }
    }
}