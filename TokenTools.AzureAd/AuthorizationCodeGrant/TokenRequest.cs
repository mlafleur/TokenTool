using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TokenTools.Core;
using TokenTools.Utils;

namespace TokenTools.AzureAd.AuthorizationCodeGrant
{
    public class TokenRequest
    {
        private AuthorizationResponse _authorizationResponse;
        private HttpClient _httpClient;

        public TokenRequest(ApplicationRegistration applicationRegistration, AuthorizationResponse authorizationResponse, [Optional]HttpClient httpClient)
        {
            if (authorizationResponse.HasErrors)
            {
                throw new HttpRequestException($"{authorizationResponse.Error} : {authorizationResponse.ErrorDescription}");
            }

            if (httpClient == null) _httpClient = new HttpClient();
            else _httpClient = httpClient;

            _authorizationResponse = authorizationResponse;

            this.ClientId = applicationRegistration.ClientId;
            this.ClientSecret = applicationRegistration.ClientSecret;
            this.RedirectUri = applicationRegistration.RedirectUri;
            this.AuthorizationCode = authorizationResponse.Code;
        }

        public string AuthorizationCode { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Resource { get; set; }
        public string Scope { get; set; }
        public string Tenant { get; set; } = "common";

        public async Task<TokenResponse> RequestToken()
        {
            var payload = new QueryParameterCollection
            {
                { "grant_type", "authorization_code" },
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri },
                { "resource", Resource},
                { "scope", Scope},
                { "code", AuthorizationCode }
            };

            // Define required query params
            var requiredParams = new List<string>() {
                "client_id",
                "client_secret",
                "redirect_uri",
                "resource",
                "code"
            };

            // Validate required values are included
            if (!payload.ValidateKeys(requiredParams))
            {
                throw new MissingValueException($"One or more required parameters are missing or empty: {string.Join(",", requiredParams.ToArray())}");
            }

            // Issue token request
            var httpResponse = await _httpClient.PostAsync($"https://login.microsoftonline.com/{Tenant}/oauth2/token", payload.ToFormUrlEncodedContent());
            if (httpResponse.IsSuccessStatusCode)
            {
                string httpContent = await httpResponse.Content.ReadAsStringAsync();
                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(httpContent);

                // If we had a state value, we persist it in our token response
                // this allows use to decode it later if the application is storing
                // value data (i.e. source url).
                if (!string.IsNullOrWhiteSpace(_authorizationResponse.State))
                {
                    tokenResponse.State = _authorizationResponse.State;
                }

                return tokenResponse;
            }
            else
            {
                throw new HttpRequestException(await httpResponse.Content.ReadAsStringAsync());
            }
        }
    }
}