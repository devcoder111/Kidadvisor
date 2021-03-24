using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor;
using KidAdvisor.Models;
using KidAdvisor.Repositories;

namespace KidAdvisor.Services
{
    public interface IUserService
    {
        UserModel GetUser(Guid businessId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = AutoMapperConfiguration.Config.CreateMapper();
        }

        public UserModel GetUser(Guid userId)
        {
            var business = this._userRepository.GetUser(userId);
            var result = _mapper.Map<UserModel>(business);
            return result;
        }
    }
}
