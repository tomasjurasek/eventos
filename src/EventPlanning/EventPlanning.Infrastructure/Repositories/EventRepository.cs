using EventPlanning.Domain.Event;
using EventPlanning.Infrastructure.Stores;
using FluentResults;

namespace EventPlanning.Infrastructure.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private readonly IEventStore _eventStore;

        public EventRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Result<EventAggregate>> FindAsync(Guid id)
        {
            var events = await _eventStore.ReadAsync(id);

            if (events is not null & events!.Any())
            {
                var @event = new EventAggregate(events!); // TODO Factory?
                return Result.Ok(@event);
            }

            return Result.Fail("NO_AGGREGATE");
        }

        public async Task<Result> StoreAsync(EventAggregate @event)
        {
            return await _eventStore.StoreAsync(@event.Id, @event.Version, @event.UncommitedEvents.ToList());
        }
    }
}
