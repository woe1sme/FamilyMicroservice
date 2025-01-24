using AutoMapper;
using Family.Application.Models.Family;

namespace Family.Application.Mapping;

public class FamilyMapping : Profile
{
    public FamilyMapping()
    {
        CreateMap<FamilyModel, Domain.Entities.Family>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<FamilyModel, Domain.Entities.Family>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FamilyName));
        CreateMap<FamilyModel, Domain.Entities.Family>()
            .ForMember(dest => dest.FamilyMembers, opt => opt.MapFrom(src => src.FamilyMembers));

        CreateMap<Domain.Entities.Family, FamilyCreateModel>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.FamilyName));
        CreateMap<Domain.Entities.Family, FamilyCreateModel>()
            .ForMember(dest => dest.FamilyMembers, opt => opt.MapFrom(src => src.FamilyMembers));
    }
}