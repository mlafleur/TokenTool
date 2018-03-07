using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Utils;

namespace TokenTool.MicrosoftV2
{
    public class AuthorizationCodeGrant
    {
        private AuthorizationCode authorizationCode;
        private HttpClient httpClient;

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

        public async Task<AccessToken> ProcessAuthorizationResponse(string queryString)
        {
            var data = new QueryParameterCollection(queryString);

            // See if we have any errors
            if (data.AllKeys.Contains("error"))
            {
                throw new Exception($"{data.Get("error")} - {data.Get("error_description")}");
            }
            else
            {
                this.authorizationCode = JsonConvert.DeserializeObject<AuthorizationCode>(data.ToJson());
            }
            return await RequestAccessToken(this.authorizationCode.Code);
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
            var q = new QueryParameterCollection
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "redirect_uri", RedirectUri },
                    { "scope", Scope },
                    { "refresh_token", refreshToken }
                };
            return await GetAccessToken(q);
        }

        /// <summary>
        /// Requests a token from the endpoint
        /// </summary>
        public async Task<AccessToken> RequestAccessToken(string authorizationCode)
        {
            var q = new QueryParameterCollection
                {
                    { "grant_type", "authorization_code" },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "redirect_uri", RedirectUri },
                    { "scope", Scope },
                    { "code", authorizationCode }
                };
            return await GetAccessToken(q);
        }

        private System.Uri GenerateAuthorizationUri()
        {
            /// Construct a Query String
            var q = new Utils.QueryParameterCollection
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
            if (q.ValidateKeys(requiredKeys) == false)
                throw new MissingFieldException($"One or more required parameters are missing or empty: {string.Join(",", requiredKeys.ToArray())}");

            return new Uri($"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/authorize?{q.ToQueryString()}");
        }

        private async Task<AccessToken> GetAccessToken(QueryParameterCollection queryParameterCollection)
        {
            var uriBuilder = new System.UriBuilder($"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/token");

            var result = await httpClient.PostAsync(uriBuilder.ToString(), queryParameterCollection.ToFormUrlEncodedContent());

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            else
            {
                return JsonConvert.DeserializeObject<AccessToken>(await result.Content.ReadAsStringAsync());
            }
        }

        private class AuthorizationCode
        {
            [JsonProperty(PropertyName = "code")]
            public string Code { get; private set; }

            [JsonProperty(PropertyName = "state")]
            public string State { get; private set; }
        }
    }
}