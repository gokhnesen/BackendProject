using AutoMapper;
using BackendProject.Data.Abstract;
using BackendProject.Data.Requests;
using BackendProject.Data.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    [ApiController]
    public class TeamsController : Controller
    {
        private readonly ITeamRepository teamRepository;
        private readonly IMapper mapper;

        public TeamsController(ITeamRepository teamRepository,IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> ListTeamsAsync()
        {
            var teams = await teamRepository.ListTeamsAsync();
            if(teams ==null || !teams.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<Team>>(teams));

        }

        [HttpGet]
        [Route("[controller]/{teamId:guid}")]
        public async Task<IActionResult> GetTeamAsync([FromRoute] Guid teamId)
        {
            var team = await teamRepository.GetTeamAsync(teamId);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Team>(team));

        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddTeamAsync([FromBody] AddTeamRequest request)
        {
            var team= await teamRepository.AddTeam(mapper.Map< Entity.DomainModels.Team >(request));

            return CreatedAtAction(nameof(GetTeamAsync), new { teamId = team.Id }, mapper.Map<Team>(team));

        }

    }
}
