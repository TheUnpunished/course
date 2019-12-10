using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentification.Models;
using Authentification.Repositories.ProjectRepository;
using Authentification.Repositories.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<Project[]> Get()
        {
            return await _projectRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Project> Get(long id)
        {
            return await _projectRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Project> Post([FromBody] Project project)
        {
            return await _projectRepository.AddAsync(project);
        }

        [HttpPut("{id}")]
        public async Task<Project> Put(long id, [FromBody] Project project)
        {
            project.Id = id;
            return await _projectRepository.EditAsync(project);
        }

        [HttpDelete("{id}")]
        public async Task<Project> Delete(long id)
        {
            return await _projectRepository.RemoveAsync(id);
        }
    }
}