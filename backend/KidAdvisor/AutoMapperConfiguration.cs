using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KidAdvisor.Entities;
using KidAdvisor.Models;

namespace KidAdvisor
{
    public class AutoMapperConfiguration
    {
        public static object AutoMapper { get; private set; }
        public static MapperConfiguration Config { get; private set; }

        public static void Configure()
        {
            Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Business, BusinessModel>().ForMember(dest => dest.Owner, source => source.Ignore());
                cfg.CreateMap<BusinessModel, Business>();

                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();
            });
        }
    }
}
