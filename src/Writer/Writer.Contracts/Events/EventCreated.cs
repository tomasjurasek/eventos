
using Writer.Contracts.Common;

namespace Writer.Contracts.Events
{
    public record EventCreated : Event
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required DateTimeOffset StartedAt { get; init; }
        public required DateTimeOffset FinishedAt { get; init; }
        public required Organizer Organizer { get; init; }
        public required Location Location { get; init; }
    }
}
