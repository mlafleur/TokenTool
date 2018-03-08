using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TokenTool.MicrosoftV2
{
    public class ImplicitGrant : v2EndpointBase
    {
        public ImplicitGrant()
        {
            this.Tenant = "common";
            this.Scope = "https://graph.microsoft.com/.default";
            this.ResponseType = "token";
            this.ResponseMode = "fragment";
            this.GrantType = "authorization_code";
            this.Nonce = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// The full URI for launching the User Consent stage of the Authentication Code Grant
        /// </summary>
        public string AuthorizationUri { get { return GenerateAuthorizationUri().ToString(); } }

        public string ClientId { get; set; }
        public string Code { get; set; }
        public string DomainHint { get; set; }
        public string GrantType { get; }
        public string LoginHint { get; set; }
        public string Nonce { get; set; }
        public string Prompt { get; set; }
        public string RedirectUri { get; set; }
        public string ResponseMode { get; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Tenant { get; set; }

        /// <summary>
        /// Convert the Implicity Grant requests to an Access Token
        /// </summary>
        /// <returns></returns>
        public AccessToken ProcessAccessToken(string jsonResult)
        {
            return BaseProcessAccessToken(jsonResult);
        }

        private System.Uri GenerateAuthorizationUri()
        {
            /// Construct a Query String
            var queryParams = new Utils.QueryParameterCollection
            {
                { "client_id", ClientId },
                { "response_type", ResponseType },
                { "scope", Scope },
                { "redirect_uri", RedirectUri },
                { "response_mode", ResponseMode },
                { "state", State },
                { "prompt", Prompt },
                { "login_hint", LoginHint },
                { "domain_hint", DomainHint },
                { "nonce", Nonce }
            };

            // Validate we have the required keys
            var requiredKeys = new List<string>() { "client_id", "response_type", "scope" };
            if (queryParams.ValidateKeys(requiredKeys) == false)
                throw new MissingFieldException($"One or more required parameters are missing or empty: {string.Join(",", requiredKeys.ToArray())}");

            return BaseAuthorizationUri(queryParams, Tenant);
        }
    }
}