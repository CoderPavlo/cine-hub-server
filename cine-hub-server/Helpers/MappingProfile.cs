using AutoMapper;
using cine_hub_server.DTOs.Auditorium;
using cine_hub_server.DTOs.Cinema;
using cine_hub_server.DTOs.Session;
using cine_hub_server.DTOs.Ticket;
using cine_hub_server.Models;
using cine_hub_server.Models;

namespace cine_hub_server.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Session, SessionResponseDto>()
               .ForMember(dest => dest.CinemaLocation, opt => opt.MapFrom(src => src.Cinema.Location))
               .ForMember(dest => dest.AuditoriumName, opt => opt.MapFrom(src => src.Auditorium.Name))
               .ForMember(dest => dest.OccupiedSeats, opt => opt.MapFrom(src => src.Tickets.Select(t => new SeatDto { RowNumber = t.RowNumber, SeatNumber = t.SeatNumber })))
               .ForMember(dest => dest.FilmName, opt => opt.MapFrom(src => src.FilmName));

            CreateMap<CreateSessionDto, Session>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Cinema, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditorium, opt => opt.Ignore()) 
                .ForMember(dest => dest.Tickets, opt => opt.Ignore()); 

            CreateMap<UpdateSessionDto, Session>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.FilmId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CinemaId, opt => opt.Ignore()) 
                .ForMember(dest => dest.AuditoriumId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Cinema, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditorium, opt => opt.Ignore()) 
                .ForMember(dest => dest.Tickets, opt => opt.Ignore()); 

            // Мапінг для Cinema
            CreateMap<Cinema, CinemaResponseDto>();

            CreateMap<CreateCinemaDto, Cinema>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditoriums, opt => opt.Ignore()); 

            CreateMap<UpdateCinemaDto, Cinema>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditoriums, opt => opt.Ignore()); 

            // Мапінг для Auditorium
            CreateMap<Auditorium, AuditoriumResponseDto>()
                .ForMember(dest => dest.CinemaName, opt => opt.MapFrom(src => src.Cinema.Location));

            CreateMap<CreateAuditoriumDto, Auditorium>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Cinema, opt => opt.Ignore()) 
                .ForMember(dest => dest.Sessions, opt => opt.Ignore());

            CreateMap<UpdateAuditoriumDto, Auditorium>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CinemaId, opt => opt.Ignore())
                .ForMember(dest => dest.Cinema, opt => opt.Ignore())
                .ForMember(dest => dest.Sessions, opt => opt.Ignore());
            // Мапінг Ticket
            CreateMap<Ticket, TicketResponseDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
               .ForMember(dest => dest.FilmName, opt => opt.MapFrom(src => src.Session.FilmName));

            
            CreateMap<CreateTicketDto, Ticket>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore()) 
                .ForMember(dest => dest.Session, opt => opt.Ignore()); 
        }
    }
}
