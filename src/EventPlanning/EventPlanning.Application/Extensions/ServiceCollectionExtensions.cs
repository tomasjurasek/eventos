using EventPlanning.Application.Commands.CreateEvent;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddMediator(cfg =>
            {
                cfg.AddConsumer<CreateEventCommandHandler>();
            });
        }
    }
}
