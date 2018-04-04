using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TokenTool.Utils;
using System.Linq;
using Newtonsoft.Json;

namespace TokenTool.Microsoft.AdminConsent
{
    public class AdminConsentResponse
    {
        [JsonProperty(PropertyName = "admin_consent")]
        public bool AdminConsented { get; private set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; private set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; private set; }

        [JsonIgnore]
        public bool HasErrors
        {
            get { return (!string.IsNullOrEmpty(Error)); }
        }

        [JsonProperty(PropertyName = "state")]
        public string State { get; private set; }

        [JsonProperty(PropertyName = "tenant")]
        public string Tenant { get; private set; }

        /// <summary>
        /// Parse an Admin Consent response
        /// </summary>
        public static AdminConsentResponse Parse(string response)
        {
            var data = new QueryParameterCollection(response);
            return JsonConvert.DeserializeObject<AdminConsentResponse>(data.ToJson());
        }
    }
}