using AutoMapper;
using BackendProject.Data.Abstract;
using BackendProject.Data.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;
        public GendersController(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]

        public async Task<IActionResult> GetGendersAsync()
        {
            var genderList = await _genderRepository.GetGenderAsync();
            if(genderList==null || !genderList.Any())
            {
                return NotFound();
            }


            return Ok(_mapper.Map<List<Gender>>(genderList));

        }
    }
}
