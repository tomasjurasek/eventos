using FluentResults;
using Wolverine;

namespace EventPlanning.Application.Commands
{
    // Do I need this?? 
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        //TODO
        public async Task<Result<CommandResult>> Handle(TCommand command, IMessageContext context)
        {
            return await HandleAsync(command);
        }

        protected abstract Task<Result<CommandResult>> HandleAsync(TCommand context);
    }

    internal interface ICommandHandler<TCommand> where TCommand : class, ICommand
    {
    }

    public record CommandResult
    {
        public Guid Id { get; set; }
    }
}
