using EventPlanning.Domain.Event;
using EventPlanning.Domain.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services.AddSingleton<IRegistrationAggregateFactory, RegistrationAggregateFactory>()
                           .AddSingleton<IEventAggregateFactory, EventAggregateFactory>();
        }
    }
}
