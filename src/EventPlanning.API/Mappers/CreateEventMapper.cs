using AutoMapper;
using EventPlanning.API.Requests.CreateEvent;
using EventPlanning.Application.Commands.CreateEvent;

namespace EventPlanning.API.Mappers
{
    public class CreateEventMapper : Profile
    {
        public CreateEventMapper()
        {
            CreateMap<CreateEventRequest, CreateEventCommand>()
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.Organizer, opt => opt.MapFrom(src => src.Organizer));
            // TODO
        }
    }
}
