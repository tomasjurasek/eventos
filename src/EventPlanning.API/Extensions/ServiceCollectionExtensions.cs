using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace EventPlanning.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedTracing(this IServiceCollection services)
        {
            Sdk.SetDefaultTextMapPropagator(new TraceContextPropagator());

            services.AddOpenTelemetry()
                            .WithTracing(builder =>
                            {
                                builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                                    .AddService("event-planning"))
                                    .AddAspNetCoreInstrumentation()
                                    .AddHttpClientInstrumentation()
                                    .AddConsoleExporter();
                            });


            // TODO AddHeaderPropagation

            return services;
        }
    }
}
