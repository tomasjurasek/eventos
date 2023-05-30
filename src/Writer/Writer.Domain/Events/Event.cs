namespace Writer.Domain.Events
{
    public abstract record Event : IEvent
    {
        public required Guid AggregateId { get; init; }

        public required DateTimeOffset CreatedAt { get; init; }
    }

    public interface IEvent
    {
        Guid AggregateId { get; }

        DateTimeOffset CreatedAt { get; }
    }
}
