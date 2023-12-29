using Microsoft.Extensions.Hosting;

namespace EventPlanning.Writer.Infrastructure
{
    internal class EventStoreListenerBackgroundService : BackgroundService
    {
        private readonly IEventStoreListener _eventStoreListener;

        public EventStoreListenerBackgroundService(IEventStoreListener eventStoreListener)
        {
            _eventStoreListener = eventStoreListener;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _eventStoreListener.StartAsync(stoppingToken);
            }
            catch (TaskCanceledException)
            {
                // Stopping
            }
            catch(Exception)
            {
                // TODO
            }
            
        }
    }
}
