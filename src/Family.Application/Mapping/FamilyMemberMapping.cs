using AutoMapper;
using Family.Application.Models.FamilyMember;
using Family.Domain.Entities;

namespace Family.Application.Mapping
{
    public class FamilyMemberMapping : Profile
    {
        public FamilyMemberMapping()
        {
            CreateMap<FamilyMember, FamilyMemberModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<FamilyMember, FamilyMemberModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<FamilyMember, FamilyMemberModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<FamilyMember, FamilyMemberModel>()
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId));

            CreateMap<FamilyMemberCreateModel, FamilyMember>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<FamilyMemberCreateModel, FamilyMember>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<FamilyMemberCreateModel, FamilyMember>()
                .ForMember(dest => dest.FamilyId, opt => opt.MapFrom(src => src.FamilyId));
        }
    }
}
