using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Writer.Application.Handlers.CreateEvent;
using Writer.Domain.Events;
using Writer.Domain.Repositories;
using Writer.Infrastructure.Repositories;

namespace Writer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<CreateEventCommandHandler>();
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                //x.UsingRabbitMq((context, cfg) =>
                //{
                //    cfg.Host("localhost", "service", h =>
                //    {
                //        h.Username("guest");
                //        h.Password("guest");
                //    });

                //    cfg.Message<IEvent>(x => x.SetEntityName("events"));
                //    cfg.Publish<IEvent>(x => x.ExchangeType = ExchangeType.Topic);

                //});
            });

            return services;
        }
    }
}
