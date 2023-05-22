using MassTransit;
using Writer.Contracts.Commands;
using Writer.Domain.Repositories;

namespace Writer.Application.CommandHandlers
{
    public class EventCommandHandler :
        IConsumer<CreateEvent>,
        IConsumer<CancelEvent>
    {
        private readonly IEventRepository _eventRepository;

        public EventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Consume(ConsumeContext<CreateEvent> context)
        {
            var @event = new Domain.Aggregates.Event(context.Message.Name,
                context.Message.Name,
                context.Message.StartedAt,
                context.Message.FinishedAt,
                null,
                null);

            await _eventRepository.StoreAsync(@event);
        }

        public async Task Consume(ConsumeContext<CancelEvent> context)
        {
            var @event = await _eventRepository.GetAsync(context.Message.Id);
            var result = @event.Cancel();

            if (result.IsSuccess)
            {
                await _eventRepository.StoreAsync(@event);
            }
            else
            {
                await context.RespondAsync(result);
            }
        }
    }
}
