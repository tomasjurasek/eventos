using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using RabbitMQ.Client;
using Reader.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(mt =>
{
    mt.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "service", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("event-created-read", re =>
        {
            re.ConfigureConsumeTopology = false;
            re.Consumer(() => new EventCreatedConsumer(context.GetService<IDistributedCache>()));
            re.Bind("event-created", e =>
            {
                e.ExchangeType = ExchangeType.Topic;
            });
        });

        cfg.ReceiveEndpoint("event-canceled-read", re =>
        {
            re.ConfigureConsumeTopology = false;
            re.Consumer(() => new EventCanceledConsumer(context.GetService<IDistributedCache>()));
            re.Bind("event-canceled", e =>
            {
                e.ExchangeType = ExchangeType.Topic;
            });
        });
    });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
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
