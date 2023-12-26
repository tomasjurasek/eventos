using EventPlanning.Writer.Application.Commands;
using EventPlanning.Writer.Domain.Common;
using EventPlanning.Writer.Domain.Event;
using EventPlanning.Writer.Infrastructure.Options;
using EventPlanning.Writer.Infrastructure.Repositories;
using MassTransit;
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
                s.ConnectionString = configuration.GetConnectionString(EventStoreOptions.Name)!;
            });

            services.AddSingleton<IAggregateRootRepository<EventAggregate>, AggregateRootRepository<EventAggregate>>();

            services.AddMediator(cfg =>
            {
                cfg.AddConsumer<CreateEventCommandHandler>(); // .Commands Namespace
            });

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
