using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidAdvisor.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public int UserNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BusinessModel> Businesses { get; set; }
    }
}
