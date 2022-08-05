using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Entity.DomainModels
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
