using EventPlanning.Domain.Event;
using EventPlanning.Infrastructure.Options;
using EventStore.Client;
using Microsoft.Extensions.Options;

namespace EventPlanning.Infrastructure.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private EventStoreClient _store;

        public EventRepository(IOptions<EventStoreOptions> eventStoreOptions)
        {
            _store = new EventStoreClient(EventStoreClientSettings.Create(eventStoreOptions.Value.ConnectionString));
        }

        public Task<EventAggregate> FindAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(EventAggregate @event)
        {
            // Check Concurrency - version
            // Store

            throw new NotImplementedException();
        }
    }
}
