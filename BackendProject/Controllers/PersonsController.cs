using AutoMapper;
using BackendProject.Data.Abstract;
using BackendProject.Data.Requests;
using BackendProject.Data.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    [ApiController]
    public class PersonsController : Controller
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(IPersonRepository personRepository,IMapper mapper, ILogger<PersonsController> logger)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        [Route("[controller]")]
      
       
        public async Task<IActionResult> GetAllPersonsAsync()
        {
            _logger.LogInformation("Hello from GetAllPersonAsync method");
            var persons= await personRepository.GetPersonsAsync();

            return Ok(mapper.Map<List<Person>>(persons));
     
        }

        [HttpGet]
        [Route("[controller]/{personId:guid}"),ActionName("GetPersonAsync")]
        public async Task<IActionResult> GetPersonAsync([FromRoute] Guid personId)
        {
            var person = await personRepository.GetPersonAsync(personId);

            if(person==null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Person>(person));

        }

        [HttpPut]
        [Route("[controller]/{personId:guid}")]
      
        public async Task<IActionResult> UpdatePersonAsync([FromRoute] Guid personId,[FromBody] UpdatePersonRequest request)
        {

            if(await personRepository.Exists(personId))
            {
               var updatedPerson = await personRepository.UpdatePerson(personId, mapper.Map<BackendProject.Entity.DomainModels.Person>(request));
                if(updatedPerson != null)
                {
                    return Ok(mapper.Map<Person>(updatedPerson));
                }
            }
            return NotFound();
        

        }

        [HttpDelete]
        [Route("[controller]/{personId:guid}")]
        [Authorize]


        public async Task<IActionResult> DeletePersonAsync([FromRoute] Guid personId)
        {

            if (await personRepository.Exists(personId))
            {
                var deletePerson = await personRepository.DeletePerson(personId);
                if (deletePerson != null)
                {
                    return Ok(mapper.Map<Person>(deletePerson));
                }
            }
            return NotFound();


        }

        [HttpPost]
        [Route("[controller]/Add")]
    
        public async Task<IActionResult> AddPersonAsync([FromBody] AddPersonRequest request)
        {
           
            var person =  await personRepository.AddPerson(mapper.Map < BackendProject.Entity.DomainModels.Person>(request));
            return CreatedAtAction(nameof(GetPersonAsync), new { personId = person.Id }, mapper.Map<Person>(person));


        }
    }
}
