using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidAdvisor.Entities
{
    public class Business : BaseEntity
    {
        public Guid BusinessId { get; set; }
        public int BusinessNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StreetAddress { get; set; }
        public string Appartement { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
