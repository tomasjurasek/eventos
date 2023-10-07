using EventPlanning.Domain.Event;
using EventPlanning.Domain.Registration;
using EventPlanning.Infrastructure.Options;
using EventPlanning.Infrastructure.Repositories;
using EventPlanning.Infrastructure.Stores;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<EventStoreOptions>(); // TODO Bind
            services.AddSingleton<IEventStore, Stores.EventStore>();
            services.AddSingleton<IRegistrationRepository, RegistrationRepository>()
                          .AddSingleton<IEventRepository, EventRepository>();

            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) =>
                {
                    // TODO
                });
            });

            return services;
        }
    }
}
