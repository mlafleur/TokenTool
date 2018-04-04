using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using TokenTool.Microsoft;
using TokenTool.Microsoft.AuthorizationCodeGrant;

namespace Dotnet_AuthCode_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private IConfiguration config;

        public IndexModel(IConfiguration configuration)
        {
            this.config = configuration;
        }

        public System.Uri AdminConsentUri { get; set; }
        public System.Uri AuthorizationUri { get; set; }

        public void OnGet()
        {
            ApplicationRegistration applicationRegistration = new ApplicationRegistration()
            {
                ClientId = config["v2Endpoint:clientId"],
                ClientSecret = config["v2Endpoint:clientSecret"],
                RedirectUri = config["v2Endpoint:redirectUri"],
                Scopes = config["v2Endpoint:scopes"]
            };

            AuthorizationRequest authorizationRequest = new AuthorizationRequest(applicationRegistration)
            {
                Prompt = "consent"
            };

            AuthorizationUri = authorizationRequest.GenerateAuthorizationUri();

            TokenTool.Microsoft.AdminConsent.AdminConsentRequest adminConsentRequest =
                new TokenTool.Microsoft.AdminConsent.AdminConsentRequest(applicationRegistration.ClientId, "http://localhost:64191/AdminConsent");
            AdminConsentUri = adminConsentRequest.GenerateAuthorizationUri();
        }
    }
}