using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Data.Requests
{
    public class UpdatePersonRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Guid GenderId { get; set; }
        public Guid TeamId { get; set; }

        public int DailyWork { get; set; }
        public int WeeklyWork { get; set; }
        public int CompletedTask { get; set; }

        public int KPIScore { get; set; }

    }
}
