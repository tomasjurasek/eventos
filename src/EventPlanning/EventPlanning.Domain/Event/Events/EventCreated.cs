using EventPlanning.Domain.Common;

namespace EventPlanning.Domain.Event.Events
{
    public record EventCreated : IDomainEvent
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Capacity { get; init; }
        public required string Description { get; init; }
        public required DateTimeOffset StartedAt { get; init; }
        public required DateTimeOffset FinishedAt { get; init; }
        public required Address Address { get; init; }
        public required Organizer Organizer { get; init; }
    }
}
