using Writer.Domain.Aggregates;
using Writer.Domain.Repositories;

namespace Writer.Infrastructure.Repositories
{
    internal class EventRepository : IEventRepository
    {
        public Task FindAsync(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(Event aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
