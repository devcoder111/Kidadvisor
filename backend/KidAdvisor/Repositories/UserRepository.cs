using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor.Entities;

namespace KidAdvisor.Repositories
{
    public interface IUserRepository
    {
        User GetUser(Guid userId);
        IEnumerable<User> GetUsers();
    }

    public class UserRepository : IUserRepository
    {
        private readonly BusinessContext _businessContext;

        public UserRepository(BusinessContext businessContext)
        {
            _businessContext = businessContext;
        }
        public IEnumerable<User> GetUsers()
        {
            var result = this._businessContext.Users;
            return result;
        }

        public User GetUser(Guid userId)
        {
            var result = this._businessContext.Users.Where(u => u.UserId == userId).Include(u => u.Businesses).FirstOrDefault();
            return result;
        }

        
    }
}
