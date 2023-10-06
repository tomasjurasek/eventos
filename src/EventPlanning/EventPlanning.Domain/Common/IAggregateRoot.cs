namespace EventPlanning.Domain.Common
{
    public interface IAggregateRoot
    {
        public string Id { get; }

        public DateTimeOffset CreatedAt { get; }

        int Version { get; }

        IEnumerable<IDomainEvent> UncommitedEvents { get; }

    }
}
