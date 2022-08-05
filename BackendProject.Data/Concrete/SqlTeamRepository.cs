using BackendProject.Data.Abstract;
using BackendProject.Entity.Context;
using BackendProject.Entity.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendProject.Data.Concrete
{
    public class SqlTeamRepository : ITeamRepository
    {
        private readonly PersonAdminContext context;

        public SqlTeamRepository(PersonAdminContext context)
        {
            this.context = context;
        }

        public async Task<Team> AddTeam(Team request)
        {
            var team = await context.Team.AddAsync(request);
            await context.SaveChangesAsync();
            return team.Entity;
        }

        public async Task<Team> GetTeamAsync(Guid teamId)
        {
            var response = new Team();

            response = await context.Team.FirstOrDefaultAsync(x => x.Id == teamId); //FirsorDefault veri varsa ilk değeri yoksa default değeri

            return response;
        }

        public async Task<List<Team>> ListTeamsAsync()
        {
            return await context.Team.ToListAsync();
        }
    }
}
