using FluentResults;

namespace EventPlanning.Domain.Event
{
    internal class EventAggregateFactory : IEventAggregateFactory
    {
        public Result<EventAggregate> Create(string id, Organizer organizer, Address address, int capacity)
        {
            try
            {
                var @event = new EventAggregate(id, organizer, address, capacity);
                return Result.Ok(@event);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }

    public interface IEventAggregateFactory
    {
        Result<EventAggregate> Create(string id, Organizer organizer, Address address, int capacity);
    }
}
