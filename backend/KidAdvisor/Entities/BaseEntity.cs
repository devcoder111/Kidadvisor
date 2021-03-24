using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidAdvisor.Entities
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime LastEditDate { get; set; }
        public Guid RecordCreatorId { get; set; }
        public Guid LastEditorId { get; set; }
    }
}
