using Writer.Domain.Aggregates;

namespace Writer.Domain.Events
{
    public record EventLocationChanged : Event
    {
        public required Location Location { get; set; }
    }
}
