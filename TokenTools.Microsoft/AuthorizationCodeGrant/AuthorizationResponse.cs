using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TokenTools.Utils;
using System.Linq;
using Newtonsoft.Json;

namespace TokenTools.Microsoft.AuthorizationCodeGrant
{
    public class AuthorizationResponse
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; private set; }

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

        /// <summary>
        /// Parse an Authorization Code response
        /// </summary>
        public static AuthorizationResponse Parse(string response)
        {
            var data = new QueryParameterCollection(response);
            return JsonConvert.DeserializeObject<AuthorizationResponse>(data.ToJson());
        }
    }
}