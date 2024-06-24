namespace WriteModel.Application.Commands
{
    public record CancelEventCommand : ICommand
    {
        public required Guid Id {  get; init; }
    }
}
