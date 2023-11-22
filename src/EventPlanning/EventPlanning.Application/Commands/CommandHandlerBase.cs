using FluentResults;
using Wolverine;

namespace EventPlanning.Application.Commands
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        public async Task Handle(TCommand command, IMessageContext context)
        {
            var result = await HandleAsync(command);
            await context.RespondToSenderAsync(result);
        }

        protected abstract Task<Result<CommandResult>> HandleAsync(TCommand context);
    }

    internal interface ICommandHandler<TCommand> where TCommand : class, ICommand
    {
    }

    public record CommandResult
    {
        public Guid Id  { get; set; }   
    }
}
