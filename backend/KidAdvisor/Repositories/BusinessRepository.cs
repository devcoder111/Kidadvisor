using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor.Entities;

namespace KidAdvisor.Repositories
{
    public interface IBusinessRepository
    {
        void Delete(Guid businessId);
        Business GetBusiness(Guid businessId);
        IEnumerable<Business> GetBusinesses();
        Business Insert(Business business);
        Business Update(Business business);
    }

    public class BusinessRepository : IBusinessRepository
    {
        private readonly BusinessContext _businessContext;

        public BusinessRepository(BusinessContext businessContext)
        {
            _businessContext = businessContext;
        }
        public IEnumerable<Business> GetBusinesses()
        {
            var result = this._businessContext.Businesses;
            return result;
        }

        public Business GetBusiness(Guid businessId)
        {
            var result = this._businessContext.Businesses.Where(b => b.BusinessId == businessId).Include(b => b.Owner).FirstOrDefault();
            return result;
        }

        public Business Update(Business business)
        {
            var res = _businessContext.SaveChanges();
            return business;
        }

        public Business Insert(Business business)
        {

            _businessContext.Businesses.Add(business);
            var res = _businessContext.SaveChanges();
            return business;
        }

        public void Delete(Guid businessId)
        {
            var business = this._businessContext.Businesses.FirstOrDefault(b => b.BusinessId == businessId);
            _businessContext.Remove(business);
            var res = _businessContext.SaveChanges();
        }
    }
}
