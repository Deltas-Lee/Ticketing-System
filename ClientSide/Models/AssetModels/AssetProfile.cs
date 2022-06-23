using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.AssetModels
{
    public class AssetProfile : Profile 
    {
        public AssetProfile()
        {
            CreateMap<Asset, EditAssetModel>();
            CreateMap<EditAssetModel, Asset>();

        }
    }
}
