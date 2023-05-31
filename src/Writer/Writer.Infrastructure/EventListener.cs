using Domain.Events;
using EventStore.Client;
using MassTransit;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Writer.Infrastructure.Settings;

namespace Writer.Infrastructure
{
    internal class EventListener : IEventListener
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;
        private EventStoreClient _store;

        public EventListener(IOptions<EventStoreSettings> settings, IPublishEndpoint publishEndpoint)
        {
            var eventStoreClientSettings = EventStoreClientSettings.Create(settings.Value.ConnectionString);
            _store = new EventStoreClient(eventStoreClientSettings);
            _publishEndpoint = publishEndpoint;
        }
        public async Task StartAsync()
        {
            await _store.SubscribeToAllAsync(FromAll.Start, EventReceivedAsync, true,
                filterOptions: new SubscriptionFilterOptions(EventTypeFilter.ExcludeSystemEvents()));
        }

        private async Task EventReceivedAsync(StreamSubscription _, ResolvedEvent resolvedEvent, CancellationToken c)
        {
            try
            {
                var type = GetType(resolvedEvent.Event.EventType);
                var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
                var @event = (IEvent)JsonSerializer.Deserialize(jsonData, type)!;

                await _publishEndpoint.Publish(type, @event);
            }
            catch (Exception ex)
            {
                // TOOD
            }
        }

        public static Type GetType(string type) => type switch
        {
            nameof(EventCreated) => typeof(EventCreated),
            nameof(EventCanceled) => typeof(EventCanceled),
        };
    }

    public interface IEventListener
    {
        Task StartAsync();
    }
}
