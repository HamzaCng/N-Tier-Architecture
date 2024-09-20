using AutoMapper;
using TDV.Application.Shared.Users;
using TDV.Application.Shared.Users.Dtos;
using TDV.DataAccess.Abstract;
using TDV.DataAccess.Context;
using TDV.DataAccess.Repositories;
using TDV.Entity.Entities.Users;

namespace TDV.Application.Users
{
    public class UserService : GenericRepository<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(TestDbContext context, IUnitOfWork unitOfWork, IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FullNameDto> GetUserFullName(int id)
        {
            var res = await _unitOfWork.Users.GetByIdAsync(id);
            return _mapper.Map<User, FullNameDto>(res);
        }
    }
}
