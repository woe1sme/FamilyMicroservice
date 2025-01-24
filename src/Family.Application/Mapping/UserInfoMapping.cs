using AutoMapper;
using Family.Application.Models.UserInfo;
using Family.Domain.Entities;

namespace Family.Application.Mapping
{
    public class UserInfoMapping : Profile
    {
        public UserInfoMapping()
        {
            CreateMap<UserInfoModel, UserInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<UserInfoModel, UserInfo>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UserInfo, UserInfoCreateModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
