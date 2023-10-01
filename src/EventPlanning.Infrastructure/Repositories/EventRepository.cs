using EventPlanning.Domain.Event;

namespace EventPlanning.Infrastructure.Repositories
{
    internal class EventRepository : IEventRepository
    {
        public Task<EventAggregate> FindAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(EventAggregate @event)
        {
            throw new NotImplementedException();
        }
    }
}
