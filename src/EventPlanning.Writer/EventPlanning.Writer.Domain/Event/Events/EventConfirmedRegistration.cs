using Simplife.Core.Events;

namespace EventPlanning.Writer.Domain.Event.Events
{
    public record EventConfirmedRegistration : IEvent
    {
        public string AggregateId { get; init; }

        public string RegistrationId { get; init; }
    }
}
