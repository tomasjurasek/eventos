namespace Writer.Contracts.Events
{
    public abstract record Event : IEvent
    {
        public required Guid Id { get; init; }

        public required DateTimeOffset CreatedAt { get; init; }
    }
}
