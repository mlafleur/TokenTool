using System;
using System.Collections.Generic;
using System.Text;
using TokenTools.Core;

namespace TokenTools.Microsoft.AdminConsent
{
    public class AdminConsentRequest
    {
        public AdminConsentRequest(ApplicationRegistration applicationRegistration)
        {
            this.ClientId = applicationRegistration.ClientId;
            this.RedirectUri = applicationRegistration.RedirectUri;
        }

        /// <summary>
        /// Creates an AdminConsentRequest instance for generating an Authorization URI
        /// </summary>
        /// <param name="clientId">The Application D that the registration portal (apps.dev.microsoft.com) assigned your app.</param>
        public AdminConsentRequest(string clientId, string redirectUri)
        {
            this.ClientId = clientId;
            this.RedirectUri = redirectUri;
        }

        /// <summary>
        /// The Application D that the registration portal (apps.dev.microsoft.com) assigned your app.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The redirect_uri of your app, where authentication responses can be sent and received by your app.
        /// It must exactly match one of the redirect_uris you registered in the portal.
        /// For native & mobile apps, you should use the default value of "https://login.microsoftonline.com/common/oauth2/nativeclient".
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// A value included in the request that will also be returned in the token response.
        /// It can be a string of any content that you wish. A randomly generated unique value is typically used for preventing cross-site request forgery attacks.
        /// The value can also encode information about the user's state in the app before the authentication request occurred, such as the page or view they were on.
        /// </summary>
        public string State { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// The Tenant property can be used to control who can sign into the application.
        /// The allowed values are "common", "organizations", "consumers", or a single Tenant's GUID
        /// </summary>
        public string Tenant { get; set; } = "common";

        /// <summary>
        /// Generates the URI to redirect a user to when launching the Authorization Code Grant authentication flow.
        /// </summary>
        /// <returns>Authroization Code Authentication URI</returns>
        public System.Uri GenerateAuthorizationUri()
        {
            /// Construct a Query String
            var queryParams = new Utils.QueryParameterCollection
            {
                { "client_id", ClientId },
                { "redirect_uri", RedirectUri },
                { "state", State }
            };

            // Define required query params
            var requiredParams = new List<string>() {
                "client_id",
                "redirect_uri"
            };

            // Validate required values are included
            if (!queryParams.ValidateKeys(requiredParams))
            {
                throw new MissingValueException($"One or more required parameters are missing or empty: {string.Join(",", requiredParams.ToArray())}");
            }

            return new Uri($"https://login.microsoftonline.com/{Tenant}/adminconsent?{queryParams.ToQueryString()}");
        }
    }
}