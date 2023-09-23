using EventPlanning.Domain.Common;

namespace EventPlanning.Domain.Event
{
    public class EventAggregate : IAggregate
    {
        public EventAggregate(string id)
        {
            Id = id;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public string Id { get; }

        public DateTimeOffset CreatedAt { get; }

    }
}
