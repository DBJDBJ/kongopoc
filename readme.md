<h1>Kongo POC</h1>

- [Console App with Microservices using Kong API Manager](#console-app-with-microservices-using-kong-api-manager)
  - [Prerequisites](#prerequisites)
- [Configure Kong](#configure-kong)
  - [Using Kong Admin API:](#using-kong-admin-api)
- [Usage](#usage)


## Console App with Microservices using Kong API Manager

The microservice ONE will return the current date and time, and microservice TWO will be a simple echo service.

This is a simple project that consists of a client console app and two microservices. The microservices are built using C# and .NET Core and are managed by the Kong API Manager. The client app allows users to interact with the microservices through the API Gateway. User authentication is done using local Windows 10 credentials. Microservice ONE returns the current date and time, while microservice TWO is a simple echo service. The console app uses Serilog to log messages to the console.

### Prerequisites

- Windows 10 operating system
- Visual Studio (2019 or later)
- .NET Core SDK (3.1 or later)
- Kong API Manager
- Serilog



## Configure Kong 
   
   - [Kong Installation Guide](https://docs.konghq.com/install/)
   - [Kong Documentation](https://docs.konghq.com/)

To configure Kong routes and plugins to handle requests from the client app and forward them to the appropriate microservices, you can use the Kong Admin API or Konga, a Kong administration GUI.

Using Konga (Kong Admin GUI):

1. Install Konga by following the installation instructions provided in the Konga documentation: https://github.com/pantsel/konga

2. Access Konga through the web interface.

3. Connect Konga to your Kong Admin API by providing the necessary connection details (Kong Admin URL, credentials, etc.).

4. Once connected, you can use Konga to configure the routes and plugins for your APIs.

5. Create an API in Konga for each microservice you want to expose.

6. For each API, configure the necessary routes to handle requests from the client app and forward them to the corresponding microservice. You can specify the route paths, methods, and upstream URLs.

7. Add any required plugins to the APIs to handle authentication, rate limiting, request/response transformations, etc.

### Using Kong Admin API:

1. Ensure that you have Kong installed and running. Refer to the Kong documentation for installation instructions: https://docs.konghq.com/install/

2. Use an HTTP client, such as cURL or Postman, to make requests to the Kong Admin API.

3. Create an API in Kong for each microservice you want to expose by sending a POST request to the Kong Admin API's `/apis` endpoint. Include the necessary details such as `name`, `upstream_url`, `uris`, etc.

4. Configure routes for each API by sending a POST request to the Kong Admin API's `/apis/{api_id}/routes` endpoint. Specify the `paths` and `methods` to match the requests from the client app and forward them to the corresponding microservice.

5. Add plugins to the APIs as needed by sending POST requests to the Kong Admin API's `/apis/{api_id}/plugins` endpoint. Include the plugin name, configuration, and other relevant details.

By using either Konga or the Kong Admin API, you can configure the routes and plugins for your APIs to handle requests from the client app and forward them to the appropriate microservices.

Remember to update your client app to make requests to the Kong API Gateway base URL, which acts as a reverse proxy for your microservices.

# Usage

The client console app provides the following functionalities:

- User login: Enter your Windows username and password to authenticate.
- Microservice ONE: Get the current date and time.
- Microservice TWO: Send a message to be echoed back.
  

The console app will communicate with the microservices through the Kong API Gateway, and the responses will be displayed in the console.

This is a basic implementation. You can further enhance and customize it according to specific requirements.

