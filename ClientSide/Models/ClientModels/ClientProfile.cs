using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.ClientModels
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, EditClientModel>();
            //.ForMember(dest => dest.ConfirmEmail,
            //           opt => opt.MapFrom(src => src.Email));
            CreateMap<EditClientModel, Client>();
        }
    }
}
