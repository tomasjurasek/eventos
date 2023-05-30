using FluentResults;
using MassTransit;

namespace Writer.Application.Handlers
{
    public abstract class CommandHandler<TCommand> : IConsumer<TCommand> where TCommand : class
    {
        public async Task Consume(ConsumeContext<TCommand> context)
        {
            var result = await ConsumeAsync(context.Message);
            await context.RespondAsync(result);
        }

        protected abstract Task<Result> ConsumeAsync(TCommand command);
    }
}
