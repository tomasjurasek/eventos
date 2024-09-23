using EventManagement.Domain.Common;
using EventManagement.Domain.Event;
using EventManagement.Infrastructure.Options;
using EventManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Marten;
using EventManagement.Application.Commands;

namespace EventManagement.Infrastructure.Extensions
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
                options.DatabaseSchemaName = "events";
                options.Connection(configuration.GetConnectionString(EventStoreOptions.Name)!);
            });

            services.AddSingleton<IAggregateRootRepository<EventAggregate>, AggregateRootRepository<EventAggregate>>();

            return services;
        }
    }
}
