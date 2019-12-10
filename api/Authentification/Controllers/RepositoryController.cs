using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentification.Models;
using Authentification.Repositories.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Authentification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ProjectRepository _projectRepository;

        public RepositoryController(IConfiguration conf, ProjectRepository projectRepository)
        {
            configuration = conf;
            _projectRepository = projectRepository;
        }
        [HttpGet("{id}")]
        public async Task<String> Get(long id) {
            var repo =(await _projectRepository.GetAsync(id)).repository;
            return configuration["GitHub"] + repo;
        }

    }
}