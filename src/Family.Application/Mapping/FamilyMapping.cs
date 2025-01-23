using AutoMapper;
using Family.Application.Models.Family;
using Family.Application.Models.FamilyMember;
using Family.Domain.Entities;

namespace Family.Application.Mapping;

public class FamilyMapping : Profile
{
    public FamilyMapping()
    {
        CreateMap<FamilyMember, FamilyMemberModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        CreateMap<FamilyMemberModel, FamilyMember>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        
        //CreateMap<FamilyMember, FamilyMemberCreateModel>();
    }
}