
using AutoMapper;
using DataLawyer.Application.DTOs;
using DataLawyer.Application.Users.Commands;
using DataLawyer.Domain.Entities;

namespace DataLawyer.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserCreateCommand>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<Process, ProcessDto>().ReverseMap();
        CreateMap<Movement, MovementDto>().ReverseMap();
        CreateMap<Area, AreaDto>().ReverseMap();
    }
}
