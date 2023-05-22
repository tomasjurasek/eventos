using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Writer.API;
using Writer.API.HostedServices;
using Writer.Application.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddHostedService<EventPublisherHostedService>();
builder.Services.Configure<EventStoreSettings>(builder.Configuration.GetSection("EventStore"));


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
