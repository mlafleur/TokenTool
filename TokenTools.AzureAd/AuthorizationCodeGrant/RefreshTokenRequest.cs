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
    public class RefreshTokenRequest
    {
        private HttpClient _httpClient;

        public RefreshTokenRequest(ApplicationRegistration applicationRegistration, string refreshToken, [Optional]HttpClient httpClient)
        {
            if (httpClient == null) _httpClient = new HttpClient();
            else _httpClient = httpClient;

            this.ClientId = applicationRegistration.ClientId;
            this.ClientSecret = applicationRegistration.ClientSecret;
            this.RefreshToken = refreshToken;
            this.RedirectUri = applicationRegistration.RedirectUri;
        }  

        public string RefreshToken { get; set; }
        public string AuthorizationCode { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Resource { get; set; }
        public string Scope { get; set; }
        public string Tenant { get; set; } = "common";

        public async Task<TokenResponse> Refresh()
        {
            var payload = new QueryParameterCollection
            {
                { "grant_type", "refresh_token" },
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri },
                { "scope", Scope },
                { "resource", Resource },
                { "refresh_token", RefreshToken }
            };

            // Define required query params
            var requiredParams = new List<string>() {
                "client_id",
                "client_secret",
                "redirect_uri",
                "resource",
                "refresh_token"
            };

            // Validate required values are included
            if (!payload.ValidateKeys(requiredParams))
            {
                throw new MissingValueException($"One or more required parameters are missing or empty: {string.Join(",", requiredParams.ToArray())}");
            }

            var uriBuilder = new System.UriBuilder($"https://login.microsoftonline.com/{Tenant}/oauth2/token");

            var result = await _httpClient.PostAsync(uriBuilder.ToString(), payload.ToFormUrlEncodedContent());

            if (!result.IsSuccessStatusCode)
                throw new Exception(await result.Content.ReadAsStringAsync());
            else
                return JsonConvert.DeserializeObject<TokenResponse>(await result.Content.ReadAsStringAsync());
        }
    }
}