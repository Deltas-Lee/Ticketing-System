using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.AgreementModels
{
    public class AgreementProfile : Profile
    {
        public AgreementProfile()
        {
            CreateMap<Agreement, EditAgreementModel>();
            //.ForMember(dest => dest.ConfirmEmail,
            //           opt => opt.MapFrom(src => src.Email));
            CreateMap<EditAgreementModel, Agreement>();
        }
    }
}
