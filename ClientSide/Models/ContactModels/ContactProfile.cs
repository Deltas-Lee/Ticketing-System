using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.ContactModels
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, EditContactModel>();
            //.ForMember(dest => dest.ConfirmEmail,
            //           opt => opt.MapFrom(src => src.Email));
            CreateMap<EditContactModel, Contact>();
        }
    }
}
