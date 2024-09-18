using AutoMapper;
using TDV.Application.Shared.Users.Dtos;
using TDV.Entity.Entities.Users;

namespace TDV.Application.Users.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, FullNameDto>()
          .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.Name + " " + src.SurName));

        }
    }
}
