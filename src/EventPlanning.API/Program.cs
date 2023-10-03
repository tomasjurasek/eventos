using EventPlanning.API.Extensions;
using EventPlanning.API.Mappers;
using EventPlanning.Application.Extensions;
using EventPlanning.Domain.Extensions;
using EventPlanning.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// TODO Metrics
// TODO Logging
// TODO IDM

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(CreateEventMapper));

builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices();

// OpenTelemetry Tracing
builder.Services.AddDistributedTracing();



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
