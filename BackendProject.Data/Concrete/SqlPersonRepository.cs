using BackendProject.Data.Abstract;
using BackendProject.Entity.Context;
using BackendProject.Entity.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BackendProject.Data.Concrete
{
    public class SqlPersonRepository : IPersonRepository
    {
        private readonly PersonAdminContext context;

        public SqlPersonRepository(PersonAdminContext context)
        {
            this.context = context;
        }

        public async Task<bool> Exists(Guid personId)
        {
           var existPerson = await context.Person.AnyAsync((System.Linq.Expressions.Expression<Func<Person, bool>>)(x => x.Id == personId));
            return existPerson;
        }

      

        public async Task<Person> GetPersonAsync(Guid personId)
        {
            var getPerson = await context.Person.Include(nameof(Gender)).Include(nameof(KPI)).Include(nameof(Team)).FirstOrDefaultAsync(x=>x.Id == personId);
            return getPerson;

        }

        public async Task<List<Person>>  GetPersonsAsync()
        {
            var listPerson = await context.Person.Include(x=>x.Gender).Include(nameof(KPI)).Include(nameof(Team)).ToListAsync();
            return listPerson;
        }
 



   

        public async Task<Person> UpdatePerson(Guid personId, Person request)
        {
            var existingPerson = await GetPersonAsync(personId);
            if(existingPerson !=null)
            {
                existingPerson.FirstName = request.FirstName;
                existingPerson.LastName = request.LastName;
                existingPerson.DateOfBirth = request.DateOfBirth;
                existingPerson.Email = request.Email;
                existingPerson.Mobile = request.Mobile;
                existingPerson.GenderId = request.GenderId;
                existingPerson.TeamId = request.TeamId;
                existingPerson.KPI.DailyWork = request.KPI.DailyWork;
                existingPerson.KPI.WeeklyWork = request.KPI.WeeklyWork;
                existingPerson.KPI.CompletedTask = request.KPI.CompletedTask;
                existingPerson.KPI.KPIScore = request.KPI.KPIScore;






                await context.SaveChangesAsync();
                return existingPerson;
            }
            return null;
        }

        public async Task<Person> DeletePerson(Guid personId)
        {
            var existingPerson = await GetPersonAsync(personId);
            if (existingPerson != null)
            {
                context.Person.Remove(existingPerson);
         


                await context.SaveChangesAsync();
                return existingPerson;
            }
            return null;
        }

        public async Task<Person> AddPerson(Person request)
        {
           var person = await context.Person.AddAsync(request);
            await context.SaveChangesAsync();
            return person.Entity;
        }

 
    }
}
