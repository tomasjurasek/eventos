using EventPlanning.Domain.Event;
using FluentResults;
using MassTransit;

namespace EventPlanning.Application.Commands.CreateEvent
{
    internal class CreateEventCommandHandler : CommandHandlerBase<CreateEventCommand>
    {
        private readonly IEventAggregateFactory _eventAggregateFactory;
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(IEventAggregateFactory eventAggregateFactory, IEventRepository eventRepository)
        {
            _eventAggregateFactory = eventAggregateFactory;
            _eventRepository = eventRepository;
        }

        protected override async Task<Result> HandleAsync(ConsumeContext<CreateEventCommand> context)
        {
            var command = context.Message;
            var result = _eventAggregateFactory.Create(command.Name, command.Description, command.Organizer, command.Address, command.Capacity);

            if (result.IsFailed)
            {
                return Result.Fail(result.Errors);
            }
            else
            {
                await _eventRepository.StoreAsync(result.Value); // TODO Result Handling
            }

            return Result.Ok();
        }
    }
}
