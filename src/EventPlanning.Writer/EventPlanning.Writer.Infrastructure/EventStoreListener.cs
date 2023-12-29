using EventPlanning.Writer.Domain.Event.Events;
using EventStore.Client;
using MassTransit;
using Microsoft.Extensions.Options;
using Simplife.Core.Events;
using System.Text.Json;
using System.Text;
using EventPlanning.Writer.Infrastructure.Options;

namespace EventPlanning.Writer.Infrastructure
{
    internal class EventStoreListener : IEventStoreListener
    {
        private readonly IBus _bus;
        private EventStoreClient _store;

        public EventStoreListener(IOptions<EventStoreOptions> settings, IBus bus)
        {
            var eventStoreClientSettings = EventStoreClientSettings.Create(settings.Value.ConnectionString);
            _store = new EventStoreClient(eventStoreClientSettings);
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // TODO Checkpoint
            await _store.SubscribeToAllAsync(FromAll.Start, EventReceivedAsync, true,
                filterOptions: new SubscriptionFilterOptions(EventTypeFilter.ExcludeSystemEvents()), cancellationToken: cancellationToken);
        }

        private async Task EventReceivedAsync(StreamSubscription _, ResolvedEvent resolvedEvent, CancellationToken cancellationToken)
        {
            var type = GetTypeFromEvent(resolvedEvent.Event.EventType);
            var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
            var @event = (IEvent)JsonSerializer.Deserialize(jsonData, type)!;

            await _bus.Publish(type, @event, cancellationToken: cancellationToken);
        }

        //TODO Dynamic??
        private Type GetTypeFromEvent(string eventType) => eventType switch
        {
            nameof(EventCreated) => typeof(EventCreated),
            nameof(EventCanceled) => typeof(EventCanceled),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
    internal interface IEventStoreListener
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
