using EventPlanning.Domain.Event;
using MassTransit;

namespace EventPlanning.Application.Commands.CreateEvent
{
    internal class CreateEventCommandHandler : IConsumer<CreateEventCommand>
    {
        private readonly IEventAggregateFactory _eventAggregateFactory;
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(IEventAggregateFactory eventAggregateFactory, IEventRepository eventRepository)
        {
            _eventAggregateFactory = eventAggregateFactory;
            _eventRepository = eventRepository;
        }

        public async Task Consume(ConsumeContext<CreateEventCommand> context)
        {
            // TODO Response 
            // TODO Metrics

            var command = context.Message;
            var result = _eventAggregateFactory.Create(command.Name, command.Description, command.Organizer, command.Address, command.Capacity);

            if (result.IsFailed)
            {

            }
            else
            {
                await _eventRepository.StoreAsync(result.Value);
            }
        }
    }
}
