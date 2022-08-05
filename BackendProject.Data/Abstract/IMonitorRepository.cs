using BackendProject.Entity.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProject.Data.Abstract
{
    public interface IMonitorRepository
    {
        List<Monitor> MonitoringDataList();
        Monitor GetMonitorData(string requestId);
    }
}
