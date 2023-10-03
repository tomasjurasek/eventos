using FluentResults;

namespace EventPlanning.Domain.Event
{
    internal class EventAggregateFactory : IEventAggregateFactory
    {
        public Result<EventAggregate> Create(string name, string description, Organizer organizer, Address address, int capacity)
        {
            return Create(Guid.NewGuid().ToString(), name, description, organizer, address, capacity);
        }

        public Result<EventAggregate> Create(string id, string name, string description, Organizer organizer, Address address, int capacity)
        {
            try
            {

                var @event = new EventAggregate(id, name, description, organizer, address, capacity);
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
        Result<EventAggregate> Create(string name, string description, Organizer organizer, Address address, int capacity);
        Result<EventAggregate> Create(string id, string name, string description, Organizer organizer, Address address, int capacity); // TODO
    }
}
