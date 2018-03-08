using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Utils;

namespace TokenTool.MicrosoftV1
{
    public class ImplicitGrant : v1EndpointBase
    {
        public ImplicitGrant()
        {
            this.Tenant = "common";
            this.ResponseType = "token";
            this.ResponseMode = "fragment";
            this.GrantType = "authorization_code";
            this.Nonce = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// The full URI for launching the User Consent stage of the Authentication Code Grant
        /// </summary>
        public string AuthorizationUri { get { return GenerateAuthorizationUri().ToString(); } }
        public string ClientId { get; set; }
        public string Code { get; set; }
        public string DomainHint { get; set; }
        public string GrantType { get; }
        public string LoginHint { get; set; }
        public string Nonce { get; set; }
        public string Prompt { get; set; }
        public string RedirectUri { get; set; }
        public string ResponseMode { get; }
        public string ResponseType { get; set; }
        public string Resource { get; set; }
        public string State { get; set; }
        public string Tenant { get; set; }

        /// <summary>
        /// Convert the Implicity Grant requests to an Access Token
        /// </summary>
        /// <returns></returns>
        public AccessToken ProcessAccessToken(string jsonResult)
        {
            return BaseProcessAccessToken(jsonResult);
        }

        private System.Uri GenerateAuthorizationUri()
        {
            /// Construct a Query String
            var queryParams = new Utils.QueryParameterCollection
            {
                { "client_id", ClientId },
                { "response_type", ResponseType },
                { "resource", Resource },
                { "redirect_uri", RedirectUri },
                { "response_mode", ResponseMode },
                { "state", State },
                { "prompt", Prompt },
                { "login_hint", LoginHint },
                { "domain_hint", DomainHint },
                { "nonce", Nonce }
            };
            return BaseAuthorizationUri(queryParams, Tenant);
        }
    }
}


