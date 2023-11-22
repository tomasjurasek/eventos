using EventPlanning.Domain.Common;
using EventPlanning.Domain.Event;
using FluentResults;

namespace EventPlanning.Application.Commands.CreateEvent
{
    public class CreateEventCommandHandler : CommandHandlerBase<CreateEventCommand>
    {
        private readonly IAggregateRootRepository<EventAggregate> _eventRepository;

        public CreateEventCommandHandler(IAggregateRootRepository<EventAggregate> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        protected override async Task<Result<CommandResult>> HandleAsync(CreateEventCommand command)
        {
            var result = EventAggregate.Create(Guid.NewGuid(), command.Name, command.Description, command.Organizer, command.Address, command.Capacity, command.StartedAt, command.FinishedAt);

            if (result.IsSuccess)
            {
                var storeResult = await _eventRepository.StoreAsync(result.Value);
                if (storeResult.IsSuccess)
                {
                    return Result.Ok(new CommandResult
                    {
                        Id = result.Value.Id
                    });
                }

                return storeResult;

            }
            return result.ToResult();
        }
    }
}
