namespace EventManagement.Application.Commands
{
    public record CancelEventCommand : ICommand
    {
        public required Guid Id {  get; init; }
    }
}
