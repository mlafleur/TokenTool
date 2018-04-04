using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Utils;

namespace TokenTool.Microsoft.v2
{
    public class ClientCredentialsGrant : V2EndpointBase
    {
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
            var queryParams = new QueryParameterCollection
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "scope", Scope },
                };
            return await BaseGetAccessToken(queryParams, Tenant);
        }
    }
}