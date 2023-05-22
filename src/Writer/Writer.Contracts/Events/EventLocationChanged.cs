using Writer.Contracts.Common;

namespace Writer.Contracts.Events
{
    public record EventLocationChanged : Event
    {
        public required Location Location { get; set; }
    }
}
