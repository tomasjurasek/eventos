using EventPlanning.Domain.Event;
using EventPlanning.Domain.Registration;
using EventPlanning.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddSingleton<IRegistrationRepository, RegistrationRepository>()
                           .AddSingleton<IEventRepository, EventRepository>();
        }
    }
}
