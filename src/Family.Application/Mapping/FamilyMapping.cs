using AutoMapper;
using Family.Application.Models.Family;

namespace Family.Application.Mapping;

public class FamilyMapping : Profile
{
    public FamilyMapping()
    {
        CreateMap<Domain.Entities.Family, FamilyModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FamilyName))
            .ForMember(dest => dest.FamilyMembers, opt => opt.MapFrom(src => src.FamilyMembers));

        CreateMap<FamilyAndFamilyHeadCreateModel, Domain.Entities.Family>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FamilyName))
            .ConstructUsing(x => new Domain.Entities.Family(x.FamilyName, Guid.Empty));
    }
}