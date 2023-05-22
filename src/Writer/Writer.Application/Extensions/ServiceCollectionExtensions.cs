using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Writer.Application.Infrastructure;
using Writer.Contracts.Events;
using Writer.Domain.Repositories;

namespace Writer.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddSingleton<IEventPublisher, EventPublisher>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CommandHandlers.EventCommandHandler>();

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "service", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.Message<EventCreated>(e => e.SetEntityName("event-created"));
                    cfg.Publish<EventCreated>(e => e.ExchangeType = ExchangeType.Topic);

                    cfg.Message<EventCanceled>(e => e.SetEntityName("event-canceled"));
                    cfg.Publish<EventCanceled>(e => e.ExchangeType = ExchangeType.Topic);

                });
            });

            return services;
        }
    }
}
