using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Utils;

namespace TokenTool.MicrosoftV2
{
    public class ClientCredentialsGrant
    {
        private HttpClient httpClient;

        public ClientCredentialsGrant(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.Tenant = "common";
            this.Scope = "https://graph.microsoft.com/.default";
        }

        public ClientCredentialsGrant()
        {
            this.httpClient = new HttpClient();
            this.Tenant = "common";
            this.Scope = "https://graph.microsoft.com/.default";
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Scope { get; set; }

        public string Tenant { get; set; }

        /// <summary>
        /// Requests a token from the endpoint
        /// </summary>
        public async Task<AccessToken> RequestAccessToken()
        {
            var q = new QueryParameterCollection
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "scope", Scope },
                };
            return await GetAccessToken(q);
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
    }
}