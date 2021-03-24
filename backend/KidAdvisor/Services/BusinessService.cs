using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor.Entities;
using KidAdvisor.Models;
using KidAdvisor.Repositories;

namespace KidAdvisor.Services
{
    public interface IBusinessService
    {
        void DeleteBusiness(Guid businessId);
        BusinessModel GetBusiness(Guid businessId);
        IEnumerable<BusinessModel> GetBusinesses();
        BusinessModel InsertBusiness(BusinessModel businessModel);
        BusinessModel UpdateBusiness(BusinessModel businessModel);
    }

    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
            _mapper = AutoMapperConfiguration.Config.CreateMapper();
        }

        public BusinessModel GetBusiness(Guid businessId)
        {
            var business = this._businessRepository.GetBusiness(businessId);
            var result = _mapper.Map<BusinessModel>(business);
            if (result != null)
            {
                result.Owner = _mapper.Map<UserModel>(business.Owner); 
            }
            return result;
        }

        public IEnumerable<BusinessModel> GetBusinesses()
        {
            var businesses = this._businessRepository.GetBusinesses();
            var result = _mapper.Map<IEnumerable<BusinessModel>>(businesses).ToList();
            result.ForEach(r => {
                var business = businesses.FirstOrDefault(b => b.BusinessId == r.BusinessId);
                r.Owner = _mapper.Map<UserModel>(business.Owner);
            });
            return result;
        }

        public BusinessModel UpdateBusiness(BusinessModel businessModel)
        {
            var business = this._businessRepository.GetBusiness(businessModel.BusinessId);
            business.Appartement = businessModel.Appartement;
            business.City = businessModel.City;
            business.Country = businessModel.Country;
            business.Description = businessModel.Description;
            business.Name = businessModel.Name;
            business.PostalCode = businessModel.PostalCode;
            business.Province = businessModel.Province;
            business.StreetAddress = businessModel.StreetAddress;

            business = this._businessRepository.Update(business);

            var result = _mapper.Map<BusinessModel>(business);

            return result;
        }


        public BusinessModel InsertBusiness(BusinessModel businessModel)
        {
            var business = _mapper.Map<Business>(businessModel);
            business = this._businessRepository.Insert(business);
            var result = _mapper.Map<BusinessModel>(business);
            return result;
        }

        public void DeleteBusiness(Guid businessId)
        {
            this._businessRepository.Delete(businessId);
        }
    }
}
