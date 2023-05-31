using Domain.Events.Models;

namespace Domain.Events
{
    public record EventLocationChanged : Event
    {
        public required Location Location { get; set; }
    }
}
