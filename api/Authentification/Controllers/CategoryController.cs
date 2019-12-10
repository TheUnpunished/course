using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentification.Models;
using Authentification.Repositories.Entities;
using Authentification.Repositories.ProjectRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.Controllers
{
    [Route("api/project/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<Category[]> Get()
        {
            return await _categoryRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(long id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Category> Post([FromBody] Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        [HttpPut("{id}")]
        public async Task<Category> Put(long id, [FromBody] Category category)
        {
            category.Id = id;
            return await _categoryRepository.EditAsync(category);
        }

        [HttpDelete("{id}")]
        public async Task<Category> Delete(long id)
        {
            return await _categoryRepository.RemoveAsync(id);
        }
    }
}