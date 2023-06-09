using AutoMapper;
using DogsHouse.BLL.DTO.Dogs;
using DogsHouse.DAL.Entities;

namespace DogsHouse.BLL.Mapping;

public class DogsProfile : Profile
{
    public DogsProfile()
    {
        CreateMap<Dog, DogDTO>()
            .ForMember(x => x.tail_length,cf => cf.MapFrom(x => x.TailLength))
            .ReverseMap();
    }
}