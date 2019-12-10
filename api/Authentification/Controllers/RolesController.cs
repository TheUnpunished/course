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
    [Route("api/user/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _rolesRepository;

        public RoleController(RoleRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public async Task<Role[]> Get()
        {
            return await _rolesRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Role> Get(long id)
        {
            return await _rolesRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Role> Post([FromBody] Role roles)
        {
            return await _rolesRepository.AddAsync(roles);
        }

        [HttpPut("{id}")]
        public async Task<Role> Put(long id, [FromBody] Role roles)
        {
            roles.Id = id;
            return await _rolesRepository.EditAsync(roles);
        }

        [HttpDelete("{id}")]
        public async Task<Role> Delete(long id)
        {
            return await _rolesRepository.RemoveAsync(id);
        }
    }
}