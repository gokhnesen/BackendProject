using BackendProject.Data.Abstract;
using BackendProject.Entity.Context;
using BackendProject.Entity.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendProject.Data.Concrete
{
    public class SqlKPIRepository:IKPIRepository
    {
        private readonly PersonAdminContext context;

        public SqlKPIRepository(PersonAdminContext context)
        {
            this.context = context;
        }

        public async Task<KPI> GetKPIAsync(Guid kpiId)
        {
            var getkpi =await context.KPI.FirstOrDefaultAsync(x => x.Id == kpiId);
            return getkpi;
        }

        public async Task<bool> KPIExists(Guid kpiId)
        {
            var exist = await context.KPI.AnyAsync(x => x.Id == kpiId);
            return exist;
        }

        public async Task<KPI> UpdateKPI(Guid kpiId, KPI request)
        {
            var existingKpi = await GetKPIAsync(kpiId);
            if (existingKpi != null)
            {
                existingKpi.DailyWork = request.DailyWork;
                existingKpi.WeeklyWork = request.WeeklyWork;
                existingKpi.CompletedTask = request.CompletedTask;
                existingKpi.KPIScore = request.KPIScore;
                await context.SaveChangesAsync();
                return existingKpi;
            }
            return null; //Response oluşturulmalı
        }

        public async Task<List<KPI>> ListKPIAsync()
        {
          var listKpi =  await context.KPI.ToListAsync();
            return listKpi;
        }
    }
}
