using System;
using System.Collections.Generic;
using System.Text;

namespace TokenTools.Microsoft
{
    public class ApplicationRegistration
    {
        /// <summary>
        /// The Application D that the registration portal (apps.dev.microsoft.com) assigned your app.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The application secret that you created in the app registration portal for your app.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The redirect_uri of your app, where authentication responses can be sent and received by your app.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// A space-separated list of scopes that you want the user to consent to.
        /// The default value is "https://graph.microsoft.com/.default" which uses the scopes provided during application registration.
        /// </summary>
        public string Scopes { get; set; } = "https://graph.microsoft.com/.default";
    }
}