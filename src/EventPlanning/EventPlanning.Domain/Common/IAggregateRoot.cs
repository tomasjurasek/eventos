namespace EventPlanning.Domain.Common
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        DateTimeOffset CreatedAt { get; }

        int Version { get; }

        IEnumerable<IDomainEvent> GetUncommittedEvents();

        void Apply(IEnumerable<IDomainEvent> events);

    }
}
