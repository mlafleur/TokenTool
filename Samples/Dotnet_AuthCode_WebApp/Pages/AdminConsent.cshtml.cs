using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TokenTools.Microsoft.AdminConsent;

namespace Dotnet_AuthCode_WebApp.Pages
{
    public class AdminConsentModel : PageModel
    {

        public AdminConsentResponse AdminConsentResponse { get; set; }

        public void OnGet()
        {
            var queryString = this.Request.QueryString.ToString();
            this.AdminConsentResponse = AdminConsentResponse.Parse(queryString);
        }
    }
}