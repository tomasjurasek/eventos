namespace Writer.Contracts.Commands
{
    public record CreateEvent : ICommand
    {
        public required string Description { get; init; }
        public required string Name { get; init; }
        public required DateTimeOffset StartedAt { get; init; }
        public required DateTimeOffset FinishedAt { get; init; }
        public required int Capacity { get; init; }
    }
}
