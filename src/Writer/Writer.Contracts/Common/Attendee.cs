using Writer.Contracts.Events;

namespace Writer.Contracts.Common
{
    public record Attendee : Event
    {
        public required string Email { get; init; }
    }
}
