using BackendProject.Data.Abstract;
using BackendProject.Data.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProject.Data.Validators
{
   public class UpdateKPIRequestValidator:AbstractValidator<UpdateKPIRequest>
    {
        public UpdateKPIRequestValidator(IKPIRepository kpiRepository)
        {
            RuleFor(x => x.DailyWork).GreaterThan(4).LessThan(9);
            RuleFor(x => x.WeeklyWork).GreaterThan(19).LessThan(41);
            RuleFor(x => x.CompletedTask).NotEmpty();
            RuleFor(x => x.KPIScore).NotEmpty();
        }
    }
}
