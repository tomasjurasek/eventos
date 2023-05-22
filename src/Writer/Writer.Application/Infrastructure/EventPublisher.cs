using EventStore.Client;
using MassTransit;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Writer.Contracts.Events;
using Writer.API;

namespace Writer.Application.Infrastructure
{
    internal class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private EventStoreClient _store;

        public EventPublisher(IOptions<EventStoreSettings> settings, IPublishEndpoint publishEndpoint)
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
            var type = EventTypeHelper.GetType(resolvedEvent.Event.EventType);
            var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
            var @event = (IEvent)JsonSerializer.Deserialize(jsonData, type);

            await _publishEndpoint.Publish(type, @event);
        }
    }
    public interface IEventPublisher
    {
        Task StartAsync();
    }
}
