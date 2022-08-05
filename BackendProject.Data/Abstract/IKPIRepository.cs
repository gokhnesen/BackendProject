
using BackendProject.Entity.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Data.Abstract
{
    public interface IKPIRepository
    {
        Task<KPI> GetKPIAsync(Guid kpiId);
        Task<List<KPI>> ListKPIAsync();
        Task<bool> KPIExists(Guid kpiId);
        Task<KPI> UpdateKPI(Guid kpiId, KPI kpi);
    }
}
