using AutoMapper;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO.DiffecultiesDtos;
using NzWalks.Api.Models.DTO.RegionsDtos;
using NzWalks.Api.Models.DTO.WalksDtos;

namespace NzWalks.Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegionDto, Region>().ReverseMap();
            CreateMap<RegionOptoionDto, Region>().ReverseMap();

            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            // Walks Automapper configuration
            CreateMap <WalkDto,Walk>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();  

            //difficulties automapper configuratoin
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
        }
    }
}
