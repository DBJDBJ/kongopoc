using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Serilog;

namespace ClientApp
{
    class Program
    {
        // Base URL of the Kong API Gateway
        static string kongBaseUrl = "http://localhost:8000";

        static string microserviceOneUrl { get { return kongBaseUrl + "/microservice-one"; } }
        static string microserviceTwoUrl { get { return kongBaseUrl + "/microservice-two"; } }

        static HttpClient authorized_http_client { get {
        // Create an instance of authorized HttpClientHanlder
        var httpClientHandler = new HttpClientHandler
        {
            Credentials = new NetworkCredential("<username>", "<password>"),
            UseDefaultCredentials = false
        };
        // and authorized http client using it
        /* var httpClient = */ return new HttpClient(httpClientHandler);
    } }

        static async Task Main(string[] args)
        {
            // Configure logging with Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                    // Make a request to the desired microservice through the Kong API Gateway
                    await CallMicroservice(microserviceOneUrl);
                    await CallMicroservice(microserviceTwoUrl);
          
            } catch (Exception ex)
            {
                Log.Error($"Exception caught in main(): {ex.Message}" );
            }
        } // Main

        static async Task CallMicroservice(string microserviceUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, microserviceOneUrl);

            // Add any necessary headers to the request
            //request.Headers.Add("Authorization", $"Bearer {accessToken}");

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
