using ArchiveTrackingSystem.Core.Dto.RoleDtos;
using ArchiveTrackingSystem.Core.Dto.UserDtos;
using ArchiveTrackingSystem.Core.Entities;
using AutoMapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<UserAddDto, User>();
        CreateMap<UserEditDto, User>();
        CreateMap<User, UserGetDto>();


        CreateMap<RoleAddDto, Role>();
        CreateMap<RoleEditDto, Role>();
        CreateMap<Role, RoleGetDto>();

    }

}
