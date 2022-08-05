using AutoMapper;
using BackendProject.Data.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Configurations.Mapper.AfterMaps
{
    public class UpdatePersonRequestAfterMap : IMappingAction<UpdatePersonRequest, Entity.DomainModels.Person>
    {
    

        public void Process(UpdatePersonRequest source, Entity.DomainModels.Person destination, ResolutionContext context)
        {
            destination.KPI = new Entity.DomainModels.KPI()
            {
                DailyWork = source.DailyWork,
                WeeklyWork = source.WeeklyWork,
                CompletedTask = source.CompletedTask,
                KPIScore = source.KPIScore,



            };
        }
    }
}
