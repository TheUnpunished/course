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
    public class SpringController : ControllerBase
    {
        private readonly SpringRepository _springRepository;

        public SpringController(SpringRepository springRepository)
        {
            _springRepository = springRepository;
        }

        [HttpGet]
        public async Task<Spring[]> Get()
        {
            return await _springRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Spring> Get(long id)
        {
            return await _springRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Spring> Post([FromBody] Spring spring)
        {
            return await _springRepository.AddAsync(spring);
        }

        [HttpPut("{id}")]
        public async Task<Spring> Put(long id, [FromBody] Spring spring)
        {
            spring.Id = id;
            return await _springRepository.EditAsync(spring);
        }

        [HttpDelete("{id}")]
        public async Task<Spring> Delete(long id)
        {
            return await _springRepository.RemoveAsync(id);
        }
    }
}