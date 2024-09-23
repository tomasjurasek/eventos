using EventManagement.Infrastructure.Extensions;
using EventManagement.Application.Extensions;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

await builder.Host.UseWolverine(o =>
{
    o.Durability.Mode = DurabilityMode.MediatorOnly;
}).StartAsync();

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

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
app.MapDefaultEndpoints();
app.Run();
