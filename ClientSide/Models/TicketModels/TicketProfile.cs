using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, EditTicketModel>();
                //.ForMember(dest => dest.ConfirmEmail,
                //           opt => opt.MapFrom(src => src.Email));
            CreateMap<EditTicketModel, Ticket>();
        }
    }
}
