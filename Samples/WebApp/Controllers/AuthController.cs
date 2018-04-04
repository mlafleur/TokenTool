using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Microsoft;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private IConfiguration config;

        public AuthController(IConfiguration configuration)
        {
            this.config = configuration;
        }

        #region v1 Endpoint Handlers

        [HttpGet("auth/v1authcode")]
        public async Task<IActionResult> GetV1AuthCodeAsync()
        {
            TokenTool.Microsoft.v1.AccessToken accessToken;
            TokenTool.Microsoft.v1.AccessToken refreshedToken;

            var queryString = this.Request.QueryString.ToString();

            var authorizationCodeGrant = new TokenTool.Microsoft.v1.AuthorizationCodeGrant()
            {
                ClientId = config["v1Endpoint:clientId"],
                ClientSecret = config["v1Endpoint:clientSecret"],
                Scope = config["v1Endpoint:scopes"],
                RedirectUri = "http://localhost:64191/auth/authcode",
                Resource = "https://graph.microsoft.com"
            };

            using (HttpClient httpClient = new HttpClient())
            {
                accessToken = await authorizationCodeGrant.ProcessAuthorizationResponse(queryString);
                refreshedToken = await authorizationCodeGrant.RefreshAccessToken(accessToken.RefreshToken);
            }

            var idToken = TokenTool.Microsoft.IDToken.Parse(accessToken.IdToken);

            return new JsonResult(new { accessToken, refreshedToken, idToken },
                new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        #endregion v1 Endpoint Handlers

        #region v2 Endpoint Handlers

        [HttpGet("auth/admin")]
        public IActionResult GetV2Admin()
        {
            var adminConsentGrant = new TokenTool.Microsoft.v2.AdminConsentGrant(config["v2Endpoint:clientId"], "http://localhost:64191/auth/admin");
            var result = adminConsentGrant.ProcessAuthorizationResponse(this.Request.QueryString.ToString());

            return new JsonResult(result, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        [HttpGet("auth/authcode")]
        public async Task<IActionResult> GetV2AuthCodeAsync()
        {
            TokenTool.Microsoft.v2.AccessToken accessToken;
            TokenTool.Microsoft.v2.AccessToken refreshedToken;

            var queryString = this.Request.QueryString.ToString();

            var authorizationCodeGrant = new TokenTool.Microsoft.v2.AuthorizationCodeGrant()
            {
                ClientId = config["v2Endpoint:clientId"],
                ClientSecret = config["v2Endpoint:clientSecret"],
                Scope = config["v2Endpoint:scopes"],
                RedirectUri = "http://localhost:64191/auth/authcode"
            };

            using (HttpClient httpClient = new HttpClient())
            {
                accessToken = await authorizationCodeGrant.ProcessAuthorizationResponse(queryString);
                refreshedToken = await authorizationCodeGrant.RefreshAccessToken(accessToken);
            }

            var idToken = TokenTool.Microsoft.IDToken.Parse(accessToken.IdToken);

            return new JsonResult(new { accessToken, refreshedToken, idToken },
                new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        [HttpGet("auth/cc")]
        public async Task<IActionResult> GetV2ClientCredentialsAsync()
        {
            var clientCredentialsGrant = new TokenTool.Microsoft.v2.ClientCredentialsGrant()
            {
                ClientId = config["v2Endpoint:clientId"],
                ClientSecret = config["v2Endpoint:clientSecret"],
            };
            var accessToken = await clientCredentialsGrant.RequestAccessToken();
            return new JsonResult(accessToken, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        [HttpGet("auth/implicit")]
        public IActionResult GetV2Implicit()
        {
            return Ok();
        }

        #endregion v2 Endpoint Handlers
    }
}