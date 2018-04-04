using Newtonsoft.Json;
using System;
using TokenTools.Microsoft.ClientCredentialsGrant;

namespace DotNet_ClientCred_Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var appReg = new TokenTools.Microsoft.ApplicationRegistration()
            {
                ClientId = "c5b373fb-7e75-4620-81e9-161c8704b838",
                ClientSecret = "fdPKI740^lpmosPDLD51@[:"
            };

            var tokenRequest = new ClientCredentialsRequest(appReg);
            var tokenResponse = tokenRequest.RequestToken().Result;

            Console.WriteLine("Token Response:");
            Console.WriteLine(JsonConvert.SerializeObject(tokenResponse, Formatting.Indented));

            Console.WriteLine();
            Console.WriteLine("Access Token Claims:");
            var accessTokenClaims = tokenResponse.DecodeAccessToken();
            Console.WriteLine(JsonConvert.SerializeObject(accessTokenClaims, Formatting.Indented));

            Console.ReadKey();
        }
    }
}