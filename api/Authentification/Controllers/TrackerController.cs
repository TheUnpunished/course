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
    public class TrackerController : ControllerBase
    {
        private readonly TrackerRepository _trackerRepository;

        public TrackerController(TrackerRepository trackerRepository)
        {
            _trackerRepository = trackerRepository;
        }

        [HttpGet]
        public async Task<Tracker[]> Get()
        {
            return await _trackerRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Tracker> Get(long id)
        {
            return await _trackerRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Tracker> Post([FromBody] Tracker tracker)
        {
            return await _trackerRepository.AddAsync(tracker);
        }

        [HttpPut("{id}")]
        public async Task<Tracker> Put(long id, [FromBody] Tracker tracker)
        {
            tracker.Id = id;
            return await _trackerRepository.EditAsync(tracker);
        }

        [HttpDelete("{id}")]
        public async Task<Tracker> Delete(long id)
        {
            return await _trackerRepository.RemoveAsync(id);
        }
    }
}