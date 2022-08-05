using BackendProject.Entity.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendProject.Data.Abstract
{
    public interface ITeamRepository
    {

        Task <Team> GetTeamAsync(Guid teamId);
        Task<List<Team>> ListTeamsAsync();
        Task<Team> AddTeam(Team team);
    }
}
