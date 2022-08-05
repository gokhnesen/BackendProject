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
    public class UpdatePersonRequestValidator:AbstractValidator<UpdatePersonRequest>
    {

        public UpdatePersonRequestValidator(IPersonRepository personRepository, IGenderRepository genderRepository, ITeamRepository teamRepository)
        {
            RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Lütfen 3 karakterden fazla giriniz").MaximumLength(15);
            RuleFor(x => x.LastName).MinimumLength(3).MaximumLength(15).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).MinimumLength(10).MaximumLength(10).WithMessage("Telefon Numarası yanlış girildi");
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = genderRepository.GetGenderAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.TeamId).NotEmpty().Must(id =>
            {
                var team = teamRepository.ListTeamsAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (team != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please select a valid Team");
            RuleFor(x => x.DailyWork).GreaterThan(4).LessThan(9);
            RuleFor(x => x.WeeklyWork).GreaterThan(19).LessThan(41);
            RuleFor(x => x.CompletedTask).NotEmpty();
            RuleFor(x => x.KPIScore).NotEmpty();

        }
    }
}
