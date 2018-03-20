using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Utils;

namespace TokenTool.MicrosoftV1
{
    public class AuthorizationCodeGrant : v1EndpointBase
    {
        private AuthorizationCode authorizationCode;

        public AuthorizationCodeGrant(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.Tenant = "common";
            this.ResponseType = "code";
            this.ResponseMode = "query";
        }

        public AuthorizationCodeGrant()
        {
            this.httpClient = new HttpClient();
            this.Tenant = "common";
            this.ResponseType = "code";
            this.ResponseMode = "query";
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

        public string Resource { get; set; }

        public string ResponseMode { get; set; }

        public string ResponseType { get; set; }

        public string State { get; set; }

        public string Tenant { get; set; }

        public async Task<AccessToken> ProcessAuthorizationResponse(string queryString)
        {
            var authCode = BaseProcessAuthResponse(queryString);
            var accessToken = await RequestAccessToken(authCode.Code);
            accessToken.State = authCode.State;
            return accessToken;
        }

        /// <summary>
        /// Requests a new token from the endpoint
        /// </summary>
        public async Task<AccessToken> RefreshAccessToken(string refreshToken)
        {
            return await RefreshAccessToken(refreshToken, Resource);
        }

        /// <summary>
        /// Requests a new token from the endpoint
        /// </summary>
        public async Task<AccessToken> RefreshAccessToken(string refreshToken, string resrouce)
        {
            var payload = new QueryParameterCollection
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "redirect_uri", RedirectUri },
                    { "resource", resrouce},
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
                    { "resource", Resource},
                    { "code", authorizationCode }
                };
            return await BaseGetAccessToken(payload, Tenant);
        }

        private System.Uri GenerateAuthorizationUri()
        {
            /// Construct a Query String
            var queryParams = new QueryParameterCollection
            {
                { "client_id", ClientId },
                { "response_type", ResponseType },
                { "redirect_uri", RedirectUri },
                { "response_mode", ResponseMode },
                { "state", State },
                { "prompt", Prompt },
                { "login_hint", LoginHint },
                { "domain_hint", DomainHint }
            };

            return BaseAuthorizationUri(queryParams, Tenant);
        }
    }
}