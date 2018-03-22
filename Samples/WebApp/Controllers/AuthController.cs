﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool;

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
            TokenTool.MicrosoftV1.AccessToken accessToken;
            TokenTool.MicrosoftV1.AccessToken refreshedToken;

            var queryString = this.Request.QueryString.ToString();

            var authorizationCodeGrant = new TokenTool.MicrosoftV1.AuthorizationCodeGrant()
            {
                ClientId = config["v1Endpoint:clientId"],
                ClientSecret = config["v1Endpoint:clientSecret"],
                Scope = config["v1Endpoint:scopes"],
                RedirectUri = "http://localhost:64191/auth/v1authcode",
                Resource = "https://graph.microsoft.com"
            };

            using (HttpClient httpClient = new HttpClient())
            {
                accessToken = await authorizationCodeGrant.ProcessAuthorizationResponse(queryString);
                refreshedToken = await authorizationCodeGrant.RefreshAccessToken(accessToken.RefreshToken);
            }


            var idToken = TokenTool.MicrosoftV1.IDToken.Parse(accessToken.IdToken);

            return new JsonResult(new { accessToken, refreshedToken, idToken },
                new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        #endregion v1 Endpoint Handlers

        #region v2 Endpoint Handlers

        [HttpGet("auth/admin")]
        public IActionResult GetV2Admin()
        {
            var adminConsentGrant = new TokenTool.MicrosoftV2.AdminConsentGrant(config["v2Endpoint:clientId"], "http://localhost:64191/auth/admin");
            var result = adminConsentGrant.ProcessAuthorizationResponse(this.Request.QueryString.ToString());

            return new JsonResult(result, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        [HttpGet("auth/authcode")]
        public async Task<IActionResult> GetV2AuthCodeAsync()
        {
            TokenTool.MicrosoftV2.AccessToken accessToken;
            TokenTool.MicrosoftV2.AccessToken refreshedToken;

            var queryString = this.Request.QueryString.ToString();

            var authorizationCodeGrant = new TokenTool.MicrosoftV2.AuthorizationCodeGrant()
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

            var idToken = TokenTool.MicrosoftV2.IDToken.Parse(accessToken.IdToken);

            return new JsonResult(new { accessToken, refreshedToken, idToken },
                new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        [HttpGet("auth/cc")]
        public async Task<IActionResult> GetV2ClientCredentialsAsync()
        {
            var clientCredentialsGrant = new TokenTool.MicrosoftV2.ClientCredentialsGrant()
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