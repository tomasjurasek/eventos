using Simplife.Domain.Events;

namespace EventPlanning.Domain.Event.Events
{
    public record EventCanceled : IEvent
    {
        public required string AggregateId { get; init; }
        public required DateTimeOffset OccurredAt { get; init; }
    }
}
