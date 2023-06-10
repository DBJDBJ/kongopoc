// ClientApp/Program.cs

using System;
using System.Net;
using RestSharp;
using Serilog;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure logging with Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            // Update the Kong API Gateway base URL
            var baseUrl = "http://localhost:8000";

            Console.WriteLine("Welcome to the Console App!");

            Console.WriteLine("Enter your Windows username:");
            var username = Console.ReadLine();

            Console.WriteLine("Enter your Windows password:");
            var password = Console.ReadLine();

            var authenticatedClient = new RestClient(baseUrl);
            authenticatedClient.Authenticator = new NtlmAuthenticator(username, password);

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Get current date and time");
            Console.WriteLine("2. Echo a message");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    GetDateTime(authenticatedClient);
                    break;
                case "2":
                    EchoMessage(authenticatedClient);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        static void GetDateTime(RestClient client)
        {
            var request = new RestRequest("/api/datetime", DataFormat.Json);
            var response = client.Get<DateTime>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Current date and time:");
                Console.WriteLine(response.Data);
            }
            else
            {
                Console.WriteLine("Failed to retrieve date and time");
            }
        }

        static void EchoMessage(RestClient client)
        {
            Console.WriteLine("Enter a message:");
            var message = Console.ReadLine();

            var request = new RestRequest("/api/echo", DataFormat.Json);
            request.AddParameter("application/json", message, ParameterType.RequestBody);
            var response = client.Post<string>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Echoed message:");
                Console.WriteLine(response.Data);
            }
            else
            {
                Console.WriteLine("Failed to echo the message");
            }
        }
    }
}