﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Data.Requests
{
    public class UpdateKPIRequest
    {
        public int DailyWork { get; set; }
        public int WeeklyWork { get; set; }
        public int CompletedTask { get; set; }

        public int KPIScore { get; set; }
    }
}
