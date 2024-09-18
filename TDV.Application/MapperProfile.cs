using AutoMapper;
using TDV.Application.Shared.Users.Dtos;
using TDV.Entity.Entities.Users;

namespace TDV.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region User
            CreateMap(typeof(User), typeof(FullNameDto)).ReverseMap();   
            CreateMap(typeof(User), typeof(CreateOrEditUserDto)).ReverseMap();
            #endregion
        }
    }
}
