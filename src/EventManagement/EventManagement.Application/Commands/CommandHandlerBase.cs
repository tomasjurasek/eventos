using FluentResults;
using Wolverine;
using Wolverine.Attributes;
[assembly: WolverineModule] // Uh?

namespace EventManagement.Application.Commands
{
    //Probably we don't need a base class for this
    [WolverineHandler]
    public abstract class CommandHandlerBase<TCommand> where TCommand : class, ICommand
    {
        public async Task HandleAsync(TCommand command, Envelope envelope, IMessageContext context)
        {
            var result = await HandleAsync(command);
            await context.RespondToSenderAsync(result);
        }

        protected abstract Task<Result<CommandResult>> HandleAsync(TCommand context);
    }
    
    public record CommandResult
    {
        public Guid Id { get; init; }
    }
}
