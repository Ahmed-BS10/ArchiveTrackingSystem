using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Dto.AddressDtos;
using ArchiveTrackingSystem.Core.Dto.ArchiveDtos;
using ArchiveTrackingSystem.Core.Dto.EmployeDtos;
using ArchiveTrackingSystem.Core.Dto.FileDtos;
using ArchiveTrackingSystem.Core.Dto.FileOutsideArchive;
using ArchiveTrackingSystem.Core.Dto.PaymentDtos;
using ArchiveTrackingSystem.Core.Dto.RoleDtos;
using ArchiveTrackingSystem.Core.Dto.UserDtos;
using ArchiveTrackingSystem.Core.Entities;
using AutoMapper;
using System.Security.Cryptography;
using File = ArchiveTrackingSystem.Core.Entities.File;

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

             .ForMember(dest => dest.typePayment, opt => opt.Ignore()) // Ignore TypePayment, will set via PaymentID if needed
             .ForMember(dest => dest.PaymentID, opt => opt.MapFrom(src => src.PaymentID)); // Map PaymentID directly

        CreateMap<Active, ActiveGetWithIccluedDto>()
            .ForMember(dest => dest.TypePaymnet,
                       opt => opt.MapFrom(src => src.typePayment != null ? src.typePayment.Name : "لا يوجد"));

        CreateMap<Active, ActiveGetWithIncludeDto>();


        CreateMap<ActiveUpdateDto, Active>()
            .ForMember(dest => dest.typePayment, opt => opt.Ignore()) // Ignore TypePayment, will set via PaymentID if needed
            .ForMember(dest => dest.PaymentID, opt => opt.MapFrom(src => src.PaymentID)); // Map PaymentID directly
        #endregion

        #region Payment Mapper

        CreateMap<Payment, PaymentGetDto>();

        CreateMap<PaymentAddDto, Payment>()
           .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(dest => dest.actives, opt => opt.Ignore()); // Ignore TypePayment, will set via PaymentID if needed
                                                                  //.ForMember(dest => dest.file, opt => opt.Ignore()); // Ignore TypePayment, will set via PaymentID if needed


        CreateMap<PaymentEditDto, Payment>()
          .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now))
          .ForMember(dest => dest.actives, opt => opt.Ignore()); // Ignore TypePayment, will set via PaymentID if needed
                                                                 //.ForMember(dest => dest.file, opt => opt.Ignore()); // Ignore TypePayment, will set via PaymentID if needed


        #endregion

        #region Employe Mapper

        CreateMap<EmployeAddDto, Employe>();

        CreateMap<EmployeUpdateDto, Employe>();

        CreateMap<Employe, EmployeGetDto>();

        #endregion

        #region Addrees Mapper

        CreateMap<AddressAddDto, Addrees>();

        CreateMap<AddressUpdateDto, Addrees>();
        #endregion

        #region File Mapper


        CreateMap<FileAddDto, File>()
             .ForMember(dest => dest.addrees, opt => opt.MapFrom(src => src.address));


        CreateMap<FileUpdateDto, File>();
        CreateMap<File, FileGetDto>();

        CreateMap<File, FileGetWithIncludeDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.addrees.City))
            .ForMember(dest => dest.Dstrict, opt => opt.MapFrom(src => src.addrees.Dstrict))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.activte != null ? src.activte.Name : "لا يوجد"))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.typePayment != null ? src.typePayment.Name : "لا يوجد"))
            .ForMember(dest => dest.Archive, opt => opt.MapFrom(src => src.archive != null ? src.archive.Name : "لا يوجد"));



        #endregion

        #region FileOutSide Mapper

        CreateMap<FileOutsideAddDto, FileOutsideArchive>();
        CreateMap<FileOutsideUpdateDto, FileOutsideArchive>();

        CreateMap<FileOutsideArchive, FileOutsideGetDto>()
           .ForMember(dest => dest.Employe, opt => opt.MapFrom(src => src.employe.Name))
           .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.file.FileNumber));

        #endregion

        #region Archive Mapper 

        CreateMap<ArchiveAddDto, Archive>();

        CreateMap<ArchiveUpdateDto, Archive>();

        CreateMap<Archive, ArchiveGetDto>()
            .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Select(x => x.FileNumber)));

        #endregion

    }

}






