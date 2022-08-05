using BackendProject.Data.Abstract;
using BackendProject.Entity.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BackendProject.Controllers
{
    [Route("[controller]")]
    public class MonitorController : ControllerBase
    {
        private readonly IMonitorRepository _monitorRepository;

        public MonitorController(IMonitorRepository monitorRepository)
        {
            _monitorRepository = monitorRepository;
        }

        [HttpGet]
        [ActionName("GetMonitor")]
        public List<Monitor> GetMonitor()
        {
            var data = _monitorRepository.MonitoringDataList();
            return data;
        }
    }
}
