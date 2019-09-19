using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Linq;
using IdentityModel;
using System.Collections.Generic;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // request token
            var tokenClient = new TokenClient(new HttpClient() { BaseAddress= new Uri("http://localhost:5000/connect/token") },new TokenClientOptions()
            {
                ClientId = "client",
                ClientSecret = "secret"
            });
            var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync();
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            Console.WriteLine(tokenResponse.Json);

            var passwordTokenClient = new TokenClient(new HttpClient() { BaseAddress = new Uri("http://localhost:5000/connect/token") }, new TokenClientOptions()
            {
                ClientId = "ro.client",
                ClientSecret = "secret"
            });
            var passwordTokenResponse = await passwordTokenClient.RequestPasswordTokenAsync("alice", "password");
            if(passwordTokenResponse.IsError)
            {
                Console.WriteLine(passwordTokenResponse.Error);
            }
            else
            {
                var keyClient = new HttpClient()
                {
                };
                var webkey = await keyClient.GetJsonWebKeySetAsync("http://localhost:5000/.well-known/openid-configuration/jwks");
                if(webkey.IsError)
                {
                    return;
                }
                var configuration = OpenIdConnectConfiguration.Create(webkey.Raw);
                var RsaParameter = new RSAParameters()
                {
                    Exponent = Convert.FromBase64String(webkey.KeySet.Keys.First().E),
                    Modulus = Base64Url.Decode(webkey.KeySet.Keys.First().N)
                };

                var rsa = new RSACryptoServiceProvider(2048);
                var publishKey = rsa.ExportParameters(false);
                var key = new RsaSecurityKey(RsaParameter);
                var validateParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };

                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var user = jwtSecurityTokenHandler.ValidateToken(passwordTokenResponse.Json.GetValue("access_token").ToString(), validateParameters, out var token);
                Console.WriteLine(passwordTokenResponse.Json.GetValue("access_token"));
            }
            Console.WriteLine("\n\n");
            Console.ReadKey();
            var client = new HttpClient();
            // call api
            //var client = new HttpClient();
            //client.SetBearerToken(tokenResponse.AccessToken);

            //var response = await client.GetAsync("http://localhost:5001/identity");
            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine(response.StatusCode);
            //}
            //else
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(JArray.Parse(content));
            //}
        }
    }
}
