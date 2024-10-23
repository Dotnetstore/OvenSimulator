using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.OpenTelemetry;

namespace Dotnetstore.OvenSimulator.WebAPI.Extensions;

internal static class StartupApplicationExtensions
{
    internal static void StartupApplication(this WebApplicationBuilder builder)
    {
        builder
            .SetupLogging();
        
        builder
            .Services
            .AddWebApi(builder.Configuration);
        
        var app = builder.BuildApplication();
        
        app
            // .UseSerilogRequestLogging()
            .UseFastEndpoints()
            .UseSwaggerGen()
            .UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        app.Run();
    }
    
    private static WebApplicationBuilder SetupLogging(this WebApplicationBuilder builder)
    {
        var levelSwitch = new LoggingLevelSwitch();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.OpenTelemetry(option =>
            {
                option.Endpoint = "http://localhost:5341/ingest/otlp/v1/logs";
                option.Protocol = OtlpProtocol.HttpProtobuf;
                option.LevelSwitch = levelSwitch;
                option.Headers = new Dictionary<string, string>
                {
                    ["X-Seq-ApiKey"] = "H1fNIbtzpfcgbhEMrfpo"
                };
            })
            .CreateLogger();

        return builder;
    }
    
    private static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        return builder.Build();
    }
}