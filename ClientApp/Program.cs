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
        static async Task Main(string[] args)
        {
            // Configure logging with Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            // Base URL of the Kong API Gateway
            string kongBaseUrl = "http://localhost:8000";

            // Create an instance of HttpClient
            var httpClientHandler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("<username>", "<password>"),
                UseDefaultCredentials = false
            };
            var httpClient = new HttpClient(httpClientHandler);

            // Make a request to the desired microservice through the Kong API Gateway
            string microserviceUrl = $"{kongBaseUrl}/microservice-one";
            var request = new HttpRequestMessage(HttpMethod.Get, microserviceUrl);

            // Add any necessary headers to the request
            // request.Headers.Add("Authorization", "Bearer <access_token>");

            // Send the request and retrieve the response
            var response = await httpClient.SendAsync(request);

            // Check the response status and content
            if (response.IsSuccessStatusCode)
            {
                // Handle the successful response
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response content: {content}");
            }
            else
            {
                // Handle the failed response
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                Console.WriteLine($"Error message: {response.ReasonPhrase}");
            }
        }
    }
}
