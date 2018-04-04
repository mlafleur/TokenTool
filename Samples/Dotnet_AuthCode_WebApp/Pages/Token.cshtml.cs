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
    public class TokenModel : PageModel
    {
        private IConfiguration config;

        public TokenModel(IConfiguration configuration)
        {
            this.config = configuration;
        }

        [BindProperty]
        public TokenResponse TokenResponse { get; set; }

        public async Task OnGetAsync()
        {
            var queryString = this.Request.QueryString.ToString();
            AuthorizationResponse authorizationResponse = AuthorizationResponse.Parse(queryString);

            ApplicationRegistration applicationRegistration = new ApplicationRegistration()
            {
                ClientId = config["v2Endpoint:clientId"],
                ClientSecret = config["v2Endpoint:clientSecret"],
                RedirectUri = config["v2Endpoint:redirectUri"],
                Scopes = config["v2Endpoint:scopes"]
            };

            TokenRequest tokenRequest = new TokenRequest(applicationRegistration, authorizationResponse);

            this.TokenResponse = await tokenRequest.RequestToken();
        }

        public async Task<IActionResult> OnGetRefreshAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            ApplicationRegistration applicationRegistration = new ApplicationRegistration()
            {
                ClientId = config["v2Endpoint:clientId"],
                ClientSecret = config["v2Endpoint:clientSecret"],
                RedirectUri = config["v2Endpoint:redirectUri"],
                Scopes = config["v2Endpoint:scopes"]
            };

            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest(applicationRegistration, id);
            this.TokenResponse = await refreshTokenRequest.Refresh();
            return Page();
        }
    }
}