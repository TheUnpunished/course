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
    public class WikiController : ControllerBase
    {
        private readonly WikiRepository _wikiRepository;

        public WikiController(WikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        [HttpGet]
        public async Task<Wiki[]> Get()
        {
            return await _wikiRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Wiki> Get(long id)
        {
            return await _wikiRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Wiki> Post([FromBody] Wiki wiki)
        {
            return await _wikiRepository.AddAsync(wiki);
        }

        [HttpPut("{id}")]
        public async Task<Wiki> Put(long id, [FromBody] Wiki wiki)
        {
            wiki.Id = id;
            return await _wikiRepository.EditAsync(wiki);
        }

        [HttpDelete("{id}")]
        public async Task<Wiki> Delete(long id)
        {
            return await _wikiRepository.RemoveAsync(id);
        }
    }
}