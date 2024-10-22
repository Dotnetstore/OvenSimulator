using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Core;

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
            .WriteTo.Seq("http://localhost:5341",
                apiKey: "H1fNIbtzpfcgbhEMrfpo",
                controlLevelSwitch: levelSwitch,
                bufferBaseFilename: @"C:\Logs\SeqBuffer")
            .CreateLogger();

        return builder;
    }
    
    private static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        return builder.Build();
    }
}