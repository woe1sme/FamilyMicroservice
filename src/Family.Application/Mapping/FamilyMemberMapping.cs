using AutoMapper;
using Family.Application.Models.Family;
using Family.Application.Models.FamilyMember;
using Family.Domain.Entities;
using Family.Domain.Entities.Enums;

namespace Family.Application.Mapping
{
    public class FamilyMemberMapping : Profile
    {
        public FamilyMemberMapping()
        {
            CreateMap<FamilyMember, FamilyMemberModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId));

            CreateMap<FamilyMemberCreateModel, FamilyMember>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => Guid.Empty))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)))
                .ConstructUsing(x => new FamilyMember(Guid.Empty, x.Name, x.UserId, (Role)Enum.Parse(typeof(Role), x.Role), Guid.Empty));
            
            CreateMap<FamilyMemberUpdateModel, FamilyMember>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Empty))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => Guid.Empty));

            CreateMap<FamilyAndFamilyHeadCreateModel, FamilyMember>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FamilyHeadUserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.FamilyHeadUserId))
                .ConstructUsing(x => new FamilyMember(Guid.Empty, x.FamilyHeadUserName, x.FamilyHeadUserId, Role.Head, Guid.Empty));
        }
    }
}
