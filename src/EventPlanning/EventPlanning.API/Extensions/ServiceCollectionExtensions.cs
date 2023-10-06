using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace EventPlanning.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTracing(this IServiceCollection services)
        {

            services.AddOpenTelemetry()
                .WithTracing(builder =>
                    {
                        builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                                .AddService("event-planning"))
                            .AddAspNetCoreInstrumentation()
                            .AddHttpClientInstrumentation()
                            .AddConsoleExporter();
                    });

            return services;
        }

        public static IServiceCollection AddMetrics(this IServiceCollection services)
        {
            // TODO singleton Meter

            services.AddOpenTelemetry()
                .WithMetrics(builder =>
                {
                    builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                            .AddService("event-planning"))
                        .AddMeter("*")
                        .AddConsoleExporter()
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();
                });

            return services;

        }
    }
}
