using Simplife.Core.Events;

namespace EventPlanning.Writer.Domain.Event.Events
{
    public record EventCanceled : IEvent
    {
        public string AggregateId { get; init; }
    }
}
