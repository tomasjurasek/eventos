using EventPlanning.API.Extensions;
using EventPlanning.Application.Extensions;
using EventPlanning.Domain.Extensions;
using EventPlanning.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// TODO HealthChecks
// TODO IDM

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices();

// OpenTelemetry Tracing
builder.Services.AddTracing();
builder.Services.AddMetrics();

builder.Services.AddLogging();

builder.Services.AddHttpLogging(config =>
{
    config.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    config.RequestBodyLogLimit = 4096;
    config.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
