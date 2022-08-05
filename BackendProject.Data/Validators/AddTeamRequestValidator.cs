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
  public  class AddTeamRequestValidator:AbstractValidator<AddTeamRequest>
    {

        public AddTeamRequestValidator(ITeamRepository teamRepository)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Department).NotEmpty();       }
    }
}
