using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TokenTool.Utils;

namespace TokenTool.Microsoft.v1
{
    public abstract class v1EndpointBase
    {
        protected HttpClient httpClient;

        protected System.Uri BaseAuthorizationUri(QueryParameterCollection queryParameters, string tenant = "common")
        {
            return new Uri($"https://login.microsoftonline.com/{tenant}/oauth2/authorize?{queryParameters.ToQueryString()}");
        }

        protected async Task<AccessToken> BaseGetAccessToken(QueryParameterCollection queryParameters, string tenant = "common")
        {
            var uriBuilder = new System.UriBuilder($"https://login.microsoftonline.com/{tenant}/oauth2/token");

            var result = await httpClient.PostAsync(uriBuilder.ToString(), queryParameters.ToFormUrlEncodedContent());

            if (!result.IsSuccessStatusCode)
                throw new Exception(await result.Content.ReadAsStringAsync());
            else
                return BaseProcessAccessToken(await result.Content.ReadAsStringAsync());
        }

        protected AccessToken BaseProcessAccessToken(string accessTokenJson)
        {
            return JsonConvert.DeserializeObject<AccessToken>(accessTokenJson);
        }

        protected AuthorizationCode BaseProcessAuthResponse(string queryString)
        {
            var data = new QueryParameterCollection(queryString);

            // See if we have any errors
            if (data.AllKeys.Contains("error"))
            {
                throw new Exception($"{data.Get("error")} - {data.Get("error_description")}");
            }
            else
            {
                return JsonConvert.DeserializeObject<AuthorizationCode>(data.ToJson());
            }
        }

        protected class AuthorizationCode
        {
            [JsonProperty(PropertyName = "code")]
            public string Code { get; private set; }

            [JsonProperty(PropertyName = "state")]
            public string State { get; private set; }
        }
    }
}