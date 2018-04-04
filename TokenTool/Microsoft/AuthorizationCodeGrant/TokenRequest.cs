using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TokenTool.Core.Exceptions;
using TokenTool.Utils;

namespace TokenTool.Microsoft.AuthorizationCodeGrant
{
    public class TokenRequest
    {
        private HttpClient _httpClient;
        private AuthorizationResponse _authorizationResponse;

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
            this.Scope = applicationRegistration.Scopes;
            this.AuthorizationCode = authorizationResponse.Code;

        }

        /// <summary>
        /// The authorization_code that you acquired in the first leg of the flow.
        /// </summary>
        public string AuthorizationCode { get; set; }

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
        /// The same code_verifier that was used to obtain the authorization_code. Required if PKCE was used in the authorization code grant request.
        /// </summary>
        public string CodeVerifier { get; set; }

        /// <summary>
        /// The same redirect_uri value that was used to acquire the authorization_code.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// A space-separated list of scopes.
        /// The scopes requested in this leg must be equivalent to or a subset of the scopes requested in the first leg.
        /// If the scopes specified in this request span multiple resource server, then the v2.0 endpoint will return a token for the resource specified in the first scope.
        /// </summary>
        public string Scope { get; set; } = "https://graph.microsoft.com/.default";

        /// <summary>
        /// The Tenant property can be used to control who can sign into the application.
        /// The allowed values are "common", "organizations", "consumers", or a single Tenant's GUID
        /// </summary>
        public string Tenant { get; set; } = "common";

        public async Task<TokenResponse> RequestToken()
        {
            var payload = new QueryParameterCollection
            {
                { "grant_type", "authorization_code" },
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri },
                { "scope", Scope },
                { "code_verifier", CodeVerifier },
                { "code", AuthorizationCode }
            };

            // Define required query params
            var requiredParams = new List<string>() {
                "client_id",
                "client_secret",
                "redirect_uri",
                "scope",
                "code"
            };

            // Validate required values are included
            if (!payload.ValidateKeys(requiredParams))
            {
                throw new MissingValueException($"One or more required parameters are missing or empty: {string.Join(",", requiredParams.ToArray())}");
            }

            // Issue token request
            var httpResponse = await _httpClient.PostAsync($"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/token", payload.ToFormUrlEncodedContent());
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