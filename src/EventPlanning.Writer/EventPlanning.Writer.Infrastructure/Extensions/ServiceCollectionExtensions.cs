using EventPlanning.Writer.Application.Commands;
using EventPlanning.Writer.Domain.Common;
using EventPlanning.Writer.Domain.Event;
using EventPlanning.Writer.Domain.Event.Events;
using EventPlanning.Writer.Infrastructure.Options;
using EventPlanning.Writer.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace EventPlanning.Writer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<EventStoreOptions>().Configure(s =>
            {
                s.ConnectionString = configuration.GetConnectionString(EventStoreOptions.Name)!;
            });

            services.AddSingleton<IAggregateRootRepository<EventAggregate>, AggregateRootRepository<EventAggregate>>();

            services.AddMediator(cfg =>
            {
                cfg.AddConsumersFromNamespaceContaining<CreateEventCommandHandler>(); // .Commands Namespace
            });

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    
                    cfg.Host(new Uri(configuration.GetConnectionString("eventBus")));

                    cfg.Message<EventCreated>(x => x.SetEntityName("event-created"));
                    cfg.Publish<EventCreated>(x => x.ExchangeType = ExchangeType.Topic);

                    cfg.Message<EventCanceled>(x => x.SetEntityName("event-canceled"));
                    cfg.Publish<EventCanceled>(x => x.ExchangeType = ExchangeType.Topic);

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddSingleton<IEventStoreListener, EventStoreListener>();
            services.AddHostedService<EventStoreListenerBackgroundService>();

            return services;
        }
    }
}
