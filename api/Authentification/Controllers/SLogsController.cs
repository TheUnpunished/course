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
    public class SLogsController : ControllerBase
    {
        private readonly SLogRepository _sLogsRepository;

        public SLogsController(SLogRepository sLogsRepository)
        {
            _sLogsRepository = sLogsRepository;
        }

        [HttpGet]
        public async Task<SLog[]> Get()
        {
            return await _sLogsRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<SLog> Get(long id)
        {
            return await _sLogsRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<SLog> Post([FromBody] SLog sLogs)
        {
            return await _sLogsRepository.AddAsync(sLogs);
        }

        [HttpPut("{id}")]
        public async Task<SLog> Put(long id, [FromBody] SLog sLogs)
        {
            sLogs.Id = id;
            return await _sLogsRepository.EditAsync(sLogs);
        }

        [HttpDelete("{id}")]
        public async Task<SLog> Delete(long id)
        {
            return await _sLogsRepository.RemoveAsync(id);
        }
    }
}