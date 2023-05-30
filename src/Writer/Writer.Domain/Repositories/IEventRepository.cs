using Writer.Domain.Aggregates;

namespace Writer.Domain.Repositories
{
    public interface IEventRepository
    {
        Task FindAsync(Guid aggregateId);
        Task StoreAsync(Event aggregate);
    }
}
