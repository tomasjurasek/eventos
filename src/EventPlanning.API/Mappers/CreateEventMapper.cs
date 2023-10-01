using AutoMapper;
using EventPlanning.API.Requests;
using EventPlanning.API.Requests.CreateEvent;
using EventPlanning.Application.Commands.CreateEvent;
using EventPlanning.Domain.Event;

namespace EventPlanning.API.Mappers
{
    public class CreateEventMapper : Profile
    {
        public CreateEventMapper()
        {
            CreateMap<CreateEventRequest, CreateEventCommand>();
            CreateMap<AddressRequest, Address>();
            CreateMap<OrganizerRequest, Organizer>();
            // TODO
        }
    }
}
