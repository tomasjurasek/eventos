using EventPlanning.Domain.Common;

namespace EventPlanning.Domain.Registration
{
    public class RegistrationAggregate : IAggregate
    {
        public RegistrationAggregate(string id)
        {
            Id = id;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public string Id { get; }

        public DateTimeOffset CreatedAt { get; }
    }
}
