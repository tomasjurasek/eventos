using Writer.Infrastructure.Extensions;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Writer.Infrastructure.Settings;
using Writer;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddInfrastructure();

        builder.Services.Configure<EventStoreSettings>(builder.Configuration.GetSection("EventStore"));
        builder.Services.AddHostedService<EventPublisherHostedService>();

        builder.Services.AddOpenTelemetry()
            .WithTracing(s =>
            {
                s.AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter();
            })
            .WithMetrics(s =>
            {
                s.AddMeter("*")
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter();
            });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}