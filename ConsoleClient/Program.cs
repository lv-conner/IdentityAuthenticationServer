using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

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
                Console.WriteLine(passwordTokenResponse.Json);
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
