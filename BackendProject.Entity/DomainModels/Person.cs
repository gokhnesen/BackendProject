using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Entity.DomainModels
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

         public Guid TeamId { get; set; }

        public Guid GenderId { get; set; }

        public Gender Gender { get; set; }

        public KPI KPI { get; set; }
        public Team Team { get; set; }
    }
}
