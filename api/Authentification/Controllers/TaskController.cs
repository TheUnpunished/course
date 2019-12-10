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
    public class TaskController : ControllerBase
    {
        private readonly TasksRepository _tasksRepository;

        public TaskController(TasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        [HttpGet]
        public async Task<Tasks[]> Get()
        {
            return await _tasksRepository.GetAllAsync();
        }
        [Route("/get")]
        [HttpGet("{id}")]
        public async Task<Tasks> Get(long id)
        {
            return await _tasksRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Tasks> Post([FromBody] Tasks tasks)
        {
            return await _tasksRepository.AddAsync(tasks);
        }

        [HttpPut("{id}")]
        public async Task<Tasks> Put(long id, [FromBody] Tasks tasks)
        {
            tasks.Id = id;
            return await _tasksRepository.EditAsync(tasks);
        }

        [HttpDelete("{id}")]
        public async Task<Tasks> Delete(long id)
        {
            return await _tasksRepository.RemoveAsync(id);
        }
    }
}