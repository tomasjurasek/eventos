namespace Writer.Contracts.Commands
{
    public record CancelEvent : ICommand
    {
        public required Guid Id { get; init; }
        public required string Reason { get; init; }
    }
}
