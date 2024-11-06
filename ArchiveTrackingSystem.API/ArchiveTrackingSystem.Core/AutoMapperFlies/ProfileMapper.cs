using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Dto.PaymentDtos;
using ArchiveTrackingSystem.Core.Dto.RoleDtos;
using ArchiveTrackingSystem.Core.Dto.UserDtos;
using ArchiveTrackingSystem.Core.Entities;
using AutoMapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {

        #region User Mapper
        CreateMap<UserAddDto, User>();
        CreateMap<UserEditDto, User>();
        CreateMap<User, UserGetDto>();
        #endregion

        #region Role Mapper

        CreateMap<RoleAddDto, Role>();
        CreateMap<RoleEditDto, Role>();
        CreateMap<Role, RoleGetDto>();
        #endregion

        #region Active Mapper

        CreateMap<ActiveAddDto, Active>()
             .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.Now)) // Set the creation time to now
             .ForMember(dest => dest.UpdateAt, opt => opt.Ignore()) // Ignore UpdateAt since it's set on update
             .ForMember(dest => dest.typePayment, opt => opt.Ignore()) // Ignore TypePayment, will set via PaymentID if needed
             .ForMember(dest => dest.PaymentID, opt => opt.MapFrom(src => src.PaymentID)); // Map PaymentID directly

        CreateMap<Active, ActiveGetWithIccluedDto>()
            .ForMember(dest => dest.TypePaymnet,
                       opt => opt.MapFrom(src => src.typePayment != null ? src.typePayment.Name : "لا يوجد"));

        CreateMap<Active, ActiveGetDto>();


        CreateMap<ActiveUpdateDto, Active>()
            .ForMember(dest => dest.CreateAt, opt => opt.Ignore()) // Set the creation time to now
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now)) // Ignore UpdateAt since it's set on update
            .ForMember(dest => dest.typePayment, opt => opt.Ignore()) // Ignore TypePayment, will set via PaymentID if needed
            .ForMember(dest => dest.PaymentID, opt => opt.MapFrom(src => src.PaymentID)); // Map PaymentID directly
        #endregion

        #region Payment Mapper

        CreateMap<TypePayment, PaymentGetDto>();

        CreateMap<PaymentAddDto, TypePayment>()
           .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(dest => dest.activte, opt => opt.Ignore()) // Ignore TypePayment, will set via PaymentID if needed
           .ForMember(dest => dest.file, opt => opt.Ignore()); // Ignore TypePayment, will set via PaymentID if needed


        CreateMap<PaymentEditDto, TypePayment>()
          .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now))
          .ForMember(dest => dest.activte, opt => opt.Ignore()) // Ignore TypePayment, will set via PaymentID if needed
          .ForMember(dest => dest.file, opt => opt.Ignore()); // Ignore TypePayment, will set via PaymentID if needed


        #endregion






    }

}



