using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Utils;

namespace TokenTool.MicrosoftV2
{
    public class AuthorizationCodeGrant : v2EndpointBase
    {
        private AuthorizationCode authorizationCode;

        public AuthorizationCodeGrant(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.Tenant = "common";
            this.Scope = "https://graph.microsoft.com/.default";
            this.ResponseType = "code";
        }

        public AuthorizationCodeGrant()
        {
            this.httpClient = new HttpClient();
            this.Tenant = "common";
            this.Scope = "https://graph.microsoft.com/.default";
            this.ResponseType = "code";
        }

        /// <summary>
        /// The full URI for launching the User Consent stage of the Authentication Code Grant
        /// </summary>
        public string AuthorizationUri { get { return GenerateAuthorizationUri().ToString(); } }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DomainHint { get; set; }
        public string LoginHint { get; set; }
        public string Prompt { get; set; }
        public string RedirectUri { get; set; }
        public string ResponseMode { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Tenant { get; set; }

        /// <summary>
        /// Process the Authorization Code returned by the provider and convert it into an Access Token
        /// </summary>
        public async Task<AccessToken> ProcessAuthorizationResponse(string queryString)
        {
            var authCode = BaseProcessAuthResponse(queryString);
            return await RequestAccessToken(authCode.Code);
        }

        /// <summary>
        /// Requests a new token from the endpoint
        /// </summary>
        public async Task<AccessToken> RefreshAccessToken(AccessToken accessToken)
        {
            Scope = accessToken.Scope;
            return await RefreshAccessToken(accessToken.RefreshToken);
        }

        /// <summary>
        /// Requests a new token from the endpoint
        /// </summary>
        public async Task<AccessToken> RefreshAccessToken(string refreshToken)
        {
            var payload = new QueryParameterCollection
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "redirect_uri", RedirectUri },
                    { "scope", Scope },
                    { "refresh_token", refreshToken }
                };
            return await BaseGetAccessToken(payload, Tenant);
        }

        /// <summary>
        /// Requests a token from the endpoint
        /// </summary>
        public async Task<AccessToken> RequestAccessToken(string authorizationCode)
        {
            var payload = new QueryParameterCollection
                {
                    { "grant_type", "authorization_code" },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "redirect_uri", RedirectUri },
                    { "scope", Scope },
                    { "code", authorizationCode }
                };
            return await BaseGetAccessToken(payload, Tenant);
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
                { "domain_hint", DomainHint }
            };

            // Validate we have the required keys
            var requiredKeys = new List<string>() { "client_id", "response_type", "scope" };
            if (queryParams.ValidateKeys(requiredKeys) == false)
                throw new MissingFieldException($"One or more required parameters are missing or empty: {string.Join(",", requiredKeys.ToArray())}");

            return BaseAuthorizationUri(queryParams, Tenant);
        }
    }
}