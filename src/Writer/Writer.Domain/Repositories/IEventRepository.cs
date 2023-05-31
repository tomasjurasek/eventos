using Writer.Domain.Aggregates;

namespace Writer.Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> FindAsync(Guid aggregateId);
        Task StoreAsync(Event aggregate);
    }
}
