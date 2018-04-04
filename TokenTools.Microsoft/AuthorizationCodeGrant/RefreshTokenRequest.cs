using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TokenTools.Core;
using TokenTools.Utils;

namespace TokenTools.Microsoft.AuthorizationCodeGrant
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
            this.Scope = applicationRegistration.Scopes;
            this.RefreshToken = refreshToken;
            this.RedirectUri = applicationRegistration.RedirectUri;
        }

        /// <summary>
        /// The Application ID that the registration portal (apps.dev.microsoft.com) assigned your app.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The application secret that you created in the app registration portal for your app.
        /// It should not be used in a native app, because client_secrets cannot be reliably stored on devices.
        /// It is required for web apps and web APIs, which have the ability to store the client_secret securely on the server side.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The same redirect_uri value that was used to acquire the authorization_code.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// The authorization_code that you acquired in the first leg of the flow.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// A space-separated list of scopes.
        /// The scopes requested in this leg must be equivalent to or a subset of the scopes requested in the first leg.
        /// If the scopes specified in this request span multiple resource server, then the v2.0 endpoint will return a token for the resource specified in the first scope.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// The Tenant property can be used to control who can sign into the application.
        /// The allowed values are "common", "organizations", "consumers", or a single Tenant's GUID
        /// </summary>
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
                { "refresh_token", RefreshToken }
            };

            // Define required query params
            var requiredParams = new List<string>() {
                "client_id",
                "client_secret",
                "redirect_uri",
                "scope",
                "refresh_token"
            };

            // Validate required values are included
            if (!payload.ValidateKeys(requiredParams))
            {
                throw new MissingValueException($"One or more required parameters are missing or empty: {string.Join(",", requiredParams.ToArray())}");
            }

            var uriBuilder = new System.UriBuilder($"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/token");

            var result = await _httpClient.PostAsync(uriBuilder.ToString(), payload.ToFormUrlEncodedContent());

            if (!result.IsSuccessStatusCode)
                throw new Exception(await result.Content.ReadAsStringAsync());
            else
                return JsonConvert.DeserializeObject<TokenResponse>(await result.Content.ReadAsStringAsync());
        }
    }
}