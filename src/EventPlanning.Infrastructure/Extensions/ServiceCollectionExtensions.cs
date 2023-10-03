using EventPlanning.Domain.Event;
using EventPlanning.Domain.Registration;
using EventPlanning.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
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
