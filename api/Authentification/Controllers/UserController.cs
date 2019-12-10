using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Authentification.Models;
using Authentification.Repositories.UserRepository;
using Authentification.Repositories.Entities;

namespace Authentification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<User[]> Get()
        {
            return await _userRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(long id)
        {
            return await _userRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<User> Post([FromBody] User user)
        {
            return await _userRepository.AddAsync(user);
        }

        [HttpPut("{id}")]
        public async Task<User> Put(long id, [FromBody] User user)
        {
            user.Id = id;
            return await _userRepository.EditAsync(user);
        }

        [HttpDelete("{id}")]
        public async Task<User> Delete(long id)
        {
            return await _userRepository.RemoveAsync(id);
        }
    }
}