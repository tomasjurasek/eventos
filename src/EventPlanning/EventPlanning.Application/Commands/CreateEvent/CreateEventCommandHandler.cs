using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event;
using FluentResults;
using MassTransit;

namespace EventPlanning.Application.Commands.CreateEvent
{
    internal class CreateEventCommandHandler : CommandHandlerBase<CreateEventCommand>
    {
        private readonly IAggregateRootRepository<EventAggregate> _eventRepository;

        public CreateEventCommandHandler(IAggregateRootRepository<EventAggregate> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        protected override async Task<Result> HandleAsync(ConsumeContext<CreateEventCommand> context)
        {
            var command = context.Message;
            var result = EventAggregate.Create(Guid.NewGuid(), command.Name, command.Description, command.Organizer, command.Address, command.Capacity, command.StartedAt, command.FinishedAt);

            return result switch
            {
                { IsSuccess: true } => await _eventRepository.StoreAsync(result.Value),
                _ => result.ToResult()
            };

        }
    }
}
