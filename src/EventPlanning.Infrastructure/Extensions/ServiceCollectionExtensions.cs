using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event;
using EventPlanning.Infrastructure.Options;
using EventPlanning.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

           

            return services;
        }
    }
}
