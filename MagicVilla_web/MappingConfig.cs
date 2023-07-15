
using AutoMapper;
using MagicVilla_web.Models.DTO;

namespace MagicVilla_web
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
