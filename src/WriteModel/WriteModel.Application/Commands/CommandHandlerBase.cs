using FluentResults;
using Wolverine;
using Wolverine.Attributes;

namespace WriteModel.Application.Commands
{
    [WolverineHandler]
    public abstract class CommandHandlerBase<TCommand> where TCommand : class, ICommand
    {
        public async Task<Result<CommandResult>> HandleAsync(TCommand command, Envelope envelope)
        {
            var result = await HandleAsync(command);
            return result;
        }

        protected abstract Task<Result<CommandResult>> HandleAsync(TCommand context);
    }

    // TODO what return
    public record CommandResult
    {
        public Guid Id { get; init; }
    }
}
