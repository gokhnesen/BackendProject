using BackendProject.Entity.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendProject.Data.Abstract
{
    public interface IPersonRepository
    {


       Task<List<Person>>  GetPersonsAsync();
        Task<Person> GetPersonAsync(Guid personId);
        Task<bool> Exists(Guid personId);
        Task<Person> UpdatePerson(Guid personId, Person person);
        Task<Person> DeletePerson(Guid personId);
        Task<Person> AddPerson(Person person);








   


    }
}
