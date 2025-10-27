using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Api.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Get Authors list
        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<AuthorDTO>>>> GetAllAuthors()
        {
            // Call the service method to get all authors
            var response = await _authorService.GetAllAuthors();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<AuthorDTO?>>> GetAuthorById(int id)
        {
            var response = await _authorService.GetAuthorById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<AuthorDTO>>> Create([FromBody] CreateAuthorDTO dto)
        {
            var response = await _authorService.CreateAuthor(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ServiceResponse<AuthorDTO>>> Update([FromBody] AuthorDTO dto)
        {
            var response = await _authorService.UpdateAuthor(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<AuthorDTO>>> Delete(int id)
        {
            var response = await _authorService.DeleteAuthor(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
