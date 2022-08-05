using AutoMapper;
using BackendProject.Data.Abstract;
using BackendProject.Data.Requests;
using BackendProject.Data.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    [ApiController]
    public class KPIController : Controller
    {
        private readonly IKPIRepository kpiRepository;
        private readonly IMapper mapper;

        public KPIController(IKPIRepository kpiRepository, IMapper mapper)
        {
            this.kpiRepository = kpiRepository;
            this.mapper = mapper;

        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> ListKPIAsync()
        {
            var scores = await kpiRepository.ListKPIAsync();

            return Ok(mapper.Map<List<KPI>>(scores));

        }

        [HttpGet]
        [Route("[controller]/{kpiId:guid}")]
        public async Task<IActionResult> GetKPIAsync([FromRoute] Guid kpiId)
        {
            var score = await kpiRepository.GetKPIAsync(kpiId);

            if (score == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<KPI>(score));

        }

        [HttpPut]
        [Route("[controller]/{kpiId:guid}")]
        public async Task<IActionResult> UpdateKPIAsync([FromRoute] Guid kpiId, [FromBody] UpdateKPIRequest request)
        {

            if (await kpiRepository.KPIExists(kpiId))
            {
                var updatedKPI = await kpiRepository.UpdateKPI(kpiId,mapper.Map< Entity.DomainModels.KPI >(request));
                if (updatedKPI != null)
                {
                    return Ok(mapper.Map<KPI>(updatedKPI));
                }
            }
            return NotFound();


        }
    }
}
