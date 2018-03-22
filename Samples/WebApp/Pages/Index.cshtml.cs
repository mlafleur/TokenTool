using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private IConfiguration config;

        public IndexModel(IConfiguration configuration)
        {
            this.config = configuration;
        }

        public string V1AuthUri { get; set; }
        public string V1ImplicitUri { get; set; }
        public string V2AdminUri { get; set; }
        public string V2AuthUri { get; set; }
        public string V2ImplicitUri { get; set; }
        public string V2OpenIdUri { get; set; }

        public void OnGet()
        {
            Setupv1();
            Setupv2();
        }

        private void Setupv1()
        {
            var authorizationCodeGrant = new TokenTool.Microsoft.v1.AuthorizationCodeGrant()
            {
                ClientId = config["v1Endpoint:clientId"],
                Resource = config["v1Endpoint:defaultResource"],
                Scope = config["v1Endpoint:scopes"],
                RedirectUri = "http://localhost:64191/auth/v1authcode",
                Prompt="login"
            };
            V1AuthUri = authorizationCodeGrant.AuthorizationUri;

            var implicitGrant = new TokenTool.Microsoft.v1.ImplicitGrant()
            {
                ClientId = config["v1Endpoint:clientId"],
                Scope = config["v1Endpoint:scopes"],
                RedirectUri = "http://localhost:64191/",
                Resource = "https://graph.microsoft.com",
                Prompt = "consent"
            };
            V1ImplicitUri = implicitGrant.AuthorizationUri;

        }

        private void Setupv2()
        {
            var adminConsentGrant = new TokenTool.Microsoft.v2.AdminConsentGrant(config["v2Endpoint:clientId"], "http://localhost:64191/auth/admin");
            V2AdminUri = adminConsentGrant.AuthorizationUri;

            var authorizationCodeGrant = new TokenTool.Microsoft.v2.AuthorizationCodeGrant()
            {
                ClientId = config["v2Endpoint:clientId"],
                Scope = config["v2Endpoint:scopes"],
                RedirectUri = "http://localhost:64191/auth/authcode",
                Prompt = "consent"
            };
            V2AuthUri = authorizationCodeGrant.AuthorizationUri;

            var implicitGrant = new TokenTool.Microsoft.v2.ImplicitGrant()
            {
                ClientId = config["v2Endpoint:clientId"],
                RedirectUri = "http://localhost:64191/",
                Scope = "user.read",
                Prompt = "consent"
            };
            V2ImplicitUri = implicitGrant.AuthorizationUri;

            var openIdImplicitGrant = new TokenTool.Microsoft.v2.ImplicitGrant()
            {
                ClientId = config["v2Endpoint:clientId"],
                RedirectUri = "http://localhost:64191/auth/implicit",
                ResponseType = "id_token+token",
                Scope = "openid email profile user.read",
                Prompt = "consent"
            };
            V2OpenIdUri = openIdImplicitGrant.AuthorizationUri;
        }
    }
}