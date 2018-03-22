using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TokenTool.Utils;

namespace TokenTool.MicrosoftV2
{
    public class AdminConsentGrant
    {
        public AdminConsentGrant(string clientId, string redirectUri)
        {
            Tenant = "common";
            ClientId = clientId;
            RedirectUri = redirectUri;
            State = string.Empty;
        }

        public AdminConsentGrant(string clientId, string redirectUri, string state)
        {
            Tenant = "common";
            ClientId = clientId;
            RedirectUri = redirectUri;
            State = state;
        }

        public AdminConsentGrant(string tenant, string clientId, string redirectUri, string state)
        {
            Tenant = tenant;
            ClientId = clientId;
            RedirectUri = redirectUri;
            State = state;
        }

        public string AuthorizationUri { get { return GenerateAdminConsentUri().ToString(); } }
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Tenant { get; set; }

        public AdminConsent ProcessAuthorizationResponse(string queryString)
        {
            var data = new QueryParameterCollection(queryString);

            // See if we have any errors
            if (data.AllKeys.Contains("error"))
            {
                throw new Exception($"{data.Get("error")} - {data.Get("error_description")}");
            }
            else
            {
                return JsonConvert.DeserializeObject<AdminConsent>(data.ToJson());
            }
        }

        private System.Uri GenerateAdminConsentUri()
        {
            /// Construct a Query String
            var q = new Utils.QueryParameterCollection
            {
                { "client_id", ClientId },
                { "redirect_uri", RedirectUri },
                { "scope", Scope},
                { "state", State }
            };

            // Validate we have the required keys
            var requiredKeys = new List<string>() { "client_id", "redirect_uri" };
            if (q.ValidateKeys(requiredKeys) == false)
                throw new MissingFieldException($"One or more required parameters are missing or empty: {string.Join(",", requiredKeys.ToArray())}");

            return new Uri($"https://login.microsoftonline.com/{Tenant}/adminconsent?{q.ToQueryString()}");
        }

        public class AdminConsent
        {
            [JsonProperty(PropertyName = "admin_consent")]
            public bool AdminConsented { get; private set; }

            [JsonProperty(PropertyName = "state")]
            public string State { get; private set; }

            [JsonProperty(PropertyName = "tenant")]
            public string Tenant { get; private set; }
        }
    }
}