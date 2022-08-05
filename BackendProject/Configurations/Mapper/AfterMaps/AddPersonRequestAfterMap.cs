using AutoMapper;
using BackendProject.Data.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Configurations.Mapper.AfterMaps
{
    public class AddPersonRequestAfterMap : IMappingAction<AddPersonRequest, BackendProject.Entity.DomainModels.Person>
    {
        public void Process(AddPersonRequest source, BackendProject.Entity.DomainModels.Person destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();

      
            destination.KPI = new BackendProject.Entity.DomainModels.KPI()
            {
                Id = Guid.NewGuid(),
                DailyWork = source.DailyWork,
                WeeklyWork = source.WeeklyWork,
                CompletedTask = source.CompletedTask,
                KPIScore = source.KPIScore,
            };

            
        }
    }
}
