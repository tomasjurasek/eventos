namespace EventPlanning.Domain.Event
{
    public interface IEventRepository
    {
        Task<EventAggregate> FindAsync(string Id);
        Task StoreAsync(EventAggregate @event);
    }
}
