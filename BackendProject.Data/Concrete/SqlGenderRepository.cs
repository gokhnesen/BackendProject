using BackendProject.Data.Abstract;
using BackendProject.Entity.Context;
using BackendProject.Entity.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProject.Data.Concrete
{
   public class SqlGenderRepository :IGenderRepository
    {
        private readonly PersonAdminContext context;
        public SqlGenderRepository(PersonAdminContext context)
        {
            this.context = context;
        }
        public async Task<List<Gender>> GetGenderAsync()
        {
           var gender=  await context.Gender.ToListAsync();
            return gender;
        }


    }
}
