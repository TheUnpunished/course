using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Authentification.Models;
using Authentification.Repositories;
using Authentification.Repositories.Entities;
using Authentification.Repositories.ProjectRepository;

namespace Authentification.Controllers
{
    [Route("api/project/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogRepository _logsRepository;

        public LogController(LogRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }

        [HttpGet]
        public async Task<Log[]> Get()
        {
            return await _logsRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Log> Get(long id)
        {
            return await _logsRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Log> Post([FromBody] Log logs)
        {
            return await _logsRepository.AddAsync(logs);
        }

        [HttpPut("{id}")]
        public async Task<Log> Put(long id, [FromBody] Log logs)
        {
            logs.Id = id;
            return await _logsRepository.EditAsync(logs);
        }

        [HttpDelete("{id}")]
        public async Task<Log> Delete(long id)
        {
            return await _logsRepository.RemoveAsync(id);
        }
    }
}