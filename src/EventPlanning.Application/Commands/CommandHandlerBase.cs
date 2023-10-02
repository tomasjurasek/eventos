using FluentResults;
using MassTransit;

namespace EventPlanning.Application.Commands
{
    internal abstract class CommandHandlerBase<TCommand> : IConsumer<TCommand> where TCommand : class, ICommand
    {
        public async Task Consume(ConsumeContext<TCommand> context)
        {
            var result = await HandleAsync(context);
            await context.RespondAsync(result);
        }

        protected abstract Task<Result> HandleAsync(ConsumeContext<TCommand> context);
    }
}
