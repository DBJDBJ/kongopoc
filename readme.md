<h1>Kongo POC</h1>

- [Configure Kong](#configure-kong)
  - [KONG in a container](#kong-in-a-container)
    - [Using Konga (Kong Admin GUI):](#using-konga-kong-admin-gui)
    - [Using Kong Admin API:](#using-kong-admin-api)
  - [Before you proceed](#before-you-proceed)
  - [Testing](#testing)


## Console App with Microservices using Kong API Manager

The microservice ONE will return the current date and time, and microservice TWO will be a simple echo service.

This is a simple project that consists of a client console app and two microservices. The microservices are built using C# and .NET Core and are managed by the Kong API Manager. The client app allows users to interact with the microservices through the API Gateway. User authentication is done using local Windows 10 credentials. Microservice ONE returns the current date and time, while microservice TWO is a simple echo service. The console app uses Serilog to log messages to the console.

### Prerequisites

- Windows 10 operating system
- Docker for Windows
- Visual Studio (2019 or later)
- .NET Core SDK (3.1 or later)
- Kong API Manager
- Serilog



# Configure Kong 

Kong is configured through yaml configuration.

## [KONG in a container](https://hub.docker.com/r/kong/kong-gateway)

For this POC you do not need Kong peristent storage. Please scroll to How to use this image without a Database.

For (many deep) details one can go to:
   
   - [Kong Installation Guide](https://docs.konghq.com/install/)
   - [Kong Documentation](https://docs.konghq.com/)

To configure Kong routes and plugins to handle requests from the client app and forward them to the appropriate microservices, you can use the Kong Admin API or Konga, a Kong administration GUI.

### Using Konga (Kong Admin GUI):

NOTE: Konga seems to be abandoned 3 years ago.
NOTE: Another GUI App is [King](https://github.com/ligreman/king), also it seems to be kind of a abandoned.
NOTE: Kong runs natively on Kubernetes thanks to its official Kubernetes Ingress Controller.

1. Install Konga by following the installation instructions provided in the Konga documentation: https://github.com/pantsel/konga

   There is also a Docker image, [but it was updated 3 years ago](https://hub.docker.com/r/pantsel/konga).

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

## Before you proceed

We use https protocol, for that we need TLS certificated. In each micro service folder do the following:

```shell
...\MicroserviceTwo>dotnet dev-certs https --trust
Trusting the HTTPS development certificate was requested. A confirmation prompt will be displayed if the certificate was not previously trusted. Click yes on the prompt to trust the certificate.
Successfully created and trusted a new HTTPS certificate.
```

## Testing

The client console app provides the following functionalities:

- User login: Enter your Windows username and password to authenticate.
- Microservice ONE: Get the current date and time.
- Microservice TWO: Send a message to be echoed back.

Follow these steps:

1. Build the Solution. Use Visual Studio.

2. Start the Microservice One and Microservice Two:
   Open two separate command prompts or terminals and navigate to the respective project folders: `MicroserviceOne` and `MicroserviceTwo`. Run the following command in each terminal to start the services:
   ```shell
   dotnet run
   ```
   Ensure that both microservices are running successfully without any errors. Here is properly running MicroServiceTwo on one of the development machines

   ![ ](../dbjcore/media/mstworunning.png)

   You can also see Serilog in action.

3. Start the Client App:
   Open another (third) command prompt or terminal and navigate to the `ClientApp` project folder. Run the following command to start the client app:
   ```shell
   dotnet run
   ```
   The client app will make a request to the Kong API Gateway, which will route the request to the appropriate microservice based on the configured routes and plugins.

   NOTE: initialy we have hardcoded the console app to call only service one.

4. Check the Console Output:
   The console output of the client app will display the response received from the microservice. Verify that the response is as expected. If there are any errors, check the console output of the microservices for any relevant error messages.

5. Test Different Scenarios:
   You can modify the client app's code in `Program.cs` to test different scenarios. For example, you can add headers, parameters, or modify the requested microservice to test different endpoints and functionalities.

6. Validate the Microservice Responses:
   You can also directly access the microservices' endpoints using their respective URLs (e.g., `http://localhost:5001` for Microservice One, `http://localhost:5002` for Microservice Two) to validate their responses independently from the client app.

By following these steps, you can test your solution by interacting with the client app and verifying the responses received from the microservices.

The console app will communicate with the microservices through the Kong API Gateway, and the responses will be displayed in the console.

This is a basic implementation. You can further enhance and customize it according to specific requirements.

<h2>&nbsp;</h2>

![](../dbjcore/media/supersimplecode.png)
![](../dbjcore/media/dbjlogo.png)


