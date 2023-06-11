// using System;
using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Serilog;

namespace ClientApp
{
    class Program
    {
        // kong config depend on Kong config :)
        // Base URL of the Kong API Gateway
        static string kongBaseUrl = "http://localhost:8000";

        static string microserviceOneUrl { get { return kongBaseUrl + "/microservice-one"; } }
        static string microserviceTwoUrl { get { return kongBaseUrl + "/microservice-two"; } }

        //
        public static string AccessToken
        {
            get
            {
                string tenant_id = "your app tennant id";
                string clientId = "you app client id";
                string clientSecret = "you app client secret";
                string authority = $"https://login.microsoftonline.com/{tenant_id}";

                // coming from ere
                // https://portal.azure.com/#view/Microsoft_AAD_RegisteredApps/ApplicationMenuBlade/~/ProtectAnAPI/appId/5bc9ca56-057f-47a5-bd8e-212527d30223/isMSAApp~/false
                string[] scopes = {
                     "your scope uri"
                };

                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority(authority)
                    .Build();

                var authResult = app.AcquireTokenForClient(scopes).ExecuteAsync();

                return authResult.Result.AccessToken;
            }
        }



        static HttpClient authorized_http_client
        {
            get
            {
                // Create an instance of authorized HttpClientHanlder
                var httpClientHandler = new HttpClientHandler
                {
                    Credentials = new NetworkCredential("<local user>", "<pwd>"),
                    UseDefaultCredentials = false
                };
                // and authorized http client using it
                /* var httpClient = */
                return new HttpClient(httpClientHandler);
            }
        }

        static async Task Main(string[] args)
        {
            // Configure logging with Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                string accessToken = AccessToken;
                // Make a request to the desired microservice through the Kong API Gateway
                await CallMicroservice(microserviceOneUrl, accessToken);
                await CallMicroservice(microserviceTwoUrl, accessToken);

            }
            catch (Exception ex)
            {
                Log.Error($"Exception caught in main(): {ex.Message}");
            }
        } // Main

        static async Task CallMicroservice(string microserviceUrl, string _jwt_token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, microserviceOneUrl);

            // Add any necessary headers to the request
            request.Headers.Add("Authorization", $"Bearer {_jwt_token}");

            // Send the request and retrieve the response
            var response = await authorized_http_client.SendAsync(request);

            // Check the response status and content
            if (response.IsSuccessStatusCode)
            {
                // Handle the successful response
                var content = await response.Content.ReadAsStringAsync();
                Log.Information($"Response from {microserviceUrl}: {content}");
            }
            else
            {
                // Handle the failed response
                Log.Error($"Request to {microserviceUrl} failed with status code: {response.StatusCode}");
                Log.Error($"Error message: {response.ReasonPhrase}");
            }
        } // CallMicroservice
    }
}
