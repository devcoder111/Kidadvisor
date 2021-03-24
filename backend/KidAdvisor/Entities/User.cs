using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidAdvisor.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public int UserNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Business> Businesses { get; set; }

    }
}
