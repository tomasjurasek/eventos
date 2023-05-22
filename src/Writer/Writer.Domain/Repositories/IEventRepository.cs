using Writer.Domain.Aggregates;

namespace Writer.Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> GetAsync(Guid id);
        Task StoreAsync(Event aggregate);
    }
}
