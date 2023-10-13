using FluentResults;

namespace EventPlanning.Domain.Event
{
    public interface IEventRepository
    {
        Task<Result<EventAggregate>> FindAsync(Guid Id);
        Task<Result> StoreAsync(EventAggregate @event);
    }
}
