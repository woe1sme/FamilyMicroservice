using AutoMapper;
using Family.Application.Models.FamilyMember;
using Family.Application.Models.UserInfo;
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
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId));

            CreateMap<FamilyMemberCreateModel, FamilyMember>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId));
            
            CreateMap<FamilyMemberUpdateModel, FamilyMember>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FamilyName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId));

            CreateMap<(FamilyMemberCreateModel fm, UserInfoModel ui), FamilyMember>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ui.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.fm.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.fm.Role)))
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.fm.FamilyId));

        }
    }
}
