using ArchiveTrackingSystem.Core.Dto;
using ArchiveTrackingSystem.Core.Entities;
using AutoMapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<UserAddDto, User>();
        CreateMap<UserEditDto, User>();
        CreateMap<User, UserGetDto>();
    }

}
