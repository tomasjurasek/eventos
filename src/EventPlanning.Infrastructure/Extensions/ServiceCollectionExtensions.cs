using EventPlanning.Application.Commands;
using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event;
using EventPlanning.Domain.Event.Events;
using EventPlanning.Infrastructure.Options;
using EventPlanning.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Marten;

namespace EventPlanning.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<EventStoreOptions>().Configure(s =>
            {
                s.ConnectionString = configuration.GetConnectionString(EventStoreOptions.Name)!;
            });

            services.AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString(EventStoreOptions.Name)!);
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

            return services;
        }
    }
}
