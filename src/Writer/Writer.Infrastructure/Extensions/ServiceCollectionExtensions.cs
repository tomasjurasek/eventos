using Domain.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Writer.Application.Handlers.CreateEvent;
using Writer.Domain.Repositories;
using Writer.Infrastructure.Repositories;

namespace Writer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEventListener, EventListener>();
            services.AddSingleton<IEventRepository, EventRepository>();

            services.AddMediator(cfg => {
                cfg.AddConsumer<CreateEventCommandHandler>();
            });
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "service", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.Message<EventCreated>(x => x.SetEntityName("event-created"));
                    cfg.Publish<EventCreated>(x => x.ExchangeType = ExchangeType.Topic);

                    cfg.Message<EventCanceled>(x => x.SetEntityName("event-canceled"));
                    cfg.Publish<EventCanceled>(x => x.ExchangeType = ExchangeType.Topic);
                });


            });

            return services;
        }
    }
}
