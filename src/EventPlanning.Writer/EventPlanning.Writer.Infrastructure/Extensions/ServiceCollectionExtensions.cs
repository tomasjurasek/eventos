using EventPlanning.Writer.Domain.Common;
using EventPlanning.Writer.Domain.Event;
using EventPlanning.Writer.Infrastructure.Options;
using EventPlanning.Writer.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Writer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<EventStoreOptions>().Configure(s =>
            {
                s.ConnectionString = configuration.GetConnectionString(EventStoreOptions.Name);
            });
            services.AddSingleton<IAggregateRootRepository<EventAggregate>, AggregateRootRepository<EventAggregate>>();


            return services;
        }
    }
}
