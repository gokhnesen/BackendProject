using BackendProject.Entity.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProject.Data.Abstract
{
    public interface IGenderRepository
    {
        Task<List<Gender>> GetGenderAsync();

    }
}
