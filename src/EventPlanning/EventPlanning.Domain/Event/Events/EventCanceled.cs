using EventPlanning.Domain.Common;

namespace EventPlanning.Domain.Event.Events
{
    public record EventCanceled : IDomainEvent
    {
        public required Guid Id { get; init; }
    }
}
