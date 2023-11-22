using EventPlanning.Application.Extensions;
using EventPlanning.Infrastructure.Extensions;
using System.Reflection;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(context => {
    context.Discovery.IncludeAssembly(Assembly.Load("EventPlanning.Application"));

});

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

app.Run();
