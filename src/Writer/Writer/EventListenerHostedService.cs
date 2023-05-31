using Writer.Infrastructure;

namespace Writer
{
    public class EventPublisherHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventPublisherHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var eventPublisher = scope.ServiceProvider.GetRequiredService<IEventListener>();
                await eventPublisher.StartAsync();
            }

        }
    }
}
