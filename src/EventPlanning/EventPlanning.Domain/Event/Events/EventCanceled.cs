using Simplife.Core.Events;

namespace EventPlanning.Domain.Event.Events
{
    public record EventCanceled : IEvent
    {
        public string AggregateId { get; init;} 
    }
}
