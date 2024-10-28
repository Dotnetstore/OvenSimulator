# OvenSimulator WebAPI
## Overview
The OvenSimulator WebAPI is a .NET-based application designed to simulate oven operations. It leverages various libraries and frameworks such as FastEndpoints, Serilog, and OpenTelemetry for logging and monitoring, and integrates with AWS SQS for messaging.

## Project Structure
The project is organized into several key components:  
* **Dotnetstore.OvenSimulator.WebAPI:** The main WebAPI project.
* **Dotnetstore.OvenSimulator.SharedKernel:** Contains shared models, services, and constants.
* **Dotnetstore.OvenSimulator.SDK:** SDK for interacting with the WebAPI.
* **Dotnetstore.OvenSimulator.WebAPI.Tests.Integration:** Integration tests for the WebAPI.

## Key Dependencies

* **FastEndpoints:** For creating fast and efficient API endpoints.
* **Serilog:** For structured logging.
* **OpenTelemetry:** For distributed tracing and monitoring.
* **AWS SDK for .NET:** For interacting with AWS services like SQS.
* **HealthChecks:** For monitoring the health of the application.

## Configuration
The application is configured using appsettings.json and environment variables. Key configurations include logging, health checks, and AWS settings.

## Logging
Logging is set up using Serilog with console and OpenTelemetry sinks. The logging configuration is defined in the SetupLogging method in StartupApplicationExtensions.

## Health Checks
Health checks are configured to provide insights into the application's health. The health check endpoint is /health, and it uses the HealthChecks.UI.Client for response formatting.

## Temperature Simulation
The temperature evolution is calculated using the following equation:

`dT/dt = (P * (10000/C))/100 - K * (T - 25)`

Where:

* T is the current temperature.
* P is the heater power percentage.
* C is the heat capacity.
* K is the heat loss coefficient.
* 25 represents ambient room temperature.

The Runge-Kutta method is used to solve this differential equation.

## Error Simulation
Error states alter the normal operation of the oven:

* **Heater Failure:** Sets the heater power to 0, simulating a complete malfunction.
* **Gradual Heater Failure:** Slowly decreases heater power over time.
* **Intermittent Sensor Failure:** Randomly alters temperature readings to simulate faulty sensors.
* **Thermostat Issue:** Temporarily alters the target temperature, creating erroneous readings.

## Key Classes and Methods
### StartupApplicationExtensions
This class contains extension methods for setting up the application.  
* **StartupApplication:** Configures logging, adds services, and sets up the application pipeline.
* **SetupLogging:** Configures Serilog for logging.
* **BuildApplication:** Builds the WebApplication.

### Program
The entry point of the application, which calls StartupApplication to configure and run the application.

### Testing
The project includes unit and integration tests to ensure the application behaves as expected.

### Unit Tests
Unit tests are written using xUnit and FluentAssertions. Example test classes include:  
* AmazonSettingsTests
* BaseAuditableEntityTests
* DataSchemeConstantsTests
* StartupApplicationExtensionsTests

### Integration Tests
Integration tests are located in the Dotnetstore.OvenSimulator.WebAPI.Tests.Integration project and ensure that the application components work together correctly.

## Running the Application
To run the application, use the following command:

`dotnet run --project src/Dotnetstore.OvenSimulator.WebAPI`

## Building and Testing
To build the project, use:

`dotnet build`

To run the tests, use:

`dotnet test`

## Deployment
The application can be deployed to any environment that supports .NET. Ensure that the necessary environment variables and configurations are set up before deployment.

## Conclusion
This documentation provides an overview of the OvenSimulator WebAPI, including its structure, key components, and how to configure, run, and test the application. For more detailed information, refer to the source code and comments within the project.
