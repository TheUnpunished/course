using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Authentification.Models;
using Authentification.Repositories.ProjectRepository;
using Authentification.Repositories.Entities;

namespace Authentification.Controllers
{
    [Route("api/project/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleRepository _peopleRepository;

        public PeopleController(PeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        public async Task<People[]> Get()
        {
            return await _peopleRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<People> Get(long id)
        {
            return await _peopleRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<People> Post([FromBody] People people)
        {
            return await _peopleRepository.AddAsync(people);
        }

        [HttpPut("{id}")]
        public async Task<People> Put(long id, [FromBody] People people)
        {
            people.Id = id;
            return await _peopleRepository.EditAsync(people);
        }

        [HttpDelete("{id}")]
        public async Task<People> Delete(long id)
        {
            return await _peopleRepository.RemoveAsync(id);
        }
    }
}