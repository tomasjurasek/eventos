using FluentResults;
using MassTransit;

namespace EventPlanning.Application.Commands
{
    public abstract class CommandHandlerBase<TCommand>: IConsumer<TCommand> where TCommand : class, ICommand
    {
        public async Task Consume(ConsumeContext<TCommand> context)
        {
            var result = await HandleAsync(context.Message);
            await context.RespondAsync(result);
        }

        protected abstract Task<Result<CommandResult>> HandleAsync(TCommand context);
    }

    // TODO what return
    public record CommandResult
    {
        public Guid Id { get; init; }
    }
}
