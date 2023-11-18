using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event;
using EventPlanning.Infrastructure.Options;
using EventPlanning.Infrastructure.Repositories;
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
            services.AddSingleton<IAggregateRootRepository<EventAggregate>, AggregateRootRepository<EventAggregate>>();

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
