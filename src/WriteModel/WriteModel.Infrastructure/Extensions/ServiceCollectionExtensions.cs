using WriteModel.Domain.Common;
using WriteModel.Domain.Event;
using WriteModel.Infrastructure.Options;
using WriteModel.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Marten;

namespace WriteModel.Infrastructure.Extensions
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
