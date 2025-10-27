using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Services.Implementatios;
using BookCatalog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetAllBooks([FromQuery] BookFilterParams filterParams)
        {
            // Call the service method to get all books
            var response = await _bookService.GetAllBooks(filterParams);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<BookDTO?>>> GetBookById(int id)
        {
            var response = await _bookService.GetBookById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("author/{authorId:int}")]
        public async Task<ActionResult<ServiceResponse<List<BookDTO>>>> GetBookByAuthor(int authorId)
        {
            var response = await _bookService.GetBooksByAuthorId(authorId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<BookDTO>>> Create([FromBody] CreateBookDTO dto)
        {
            var response = await _bookService.CreateBook(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ServiceResponse<BookDTO>>> Update([FromBody] UpdateBookDTO dto)
        {
            var response = await _bookService.UpdateBook(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<BookDTO>>> Delete(int id)
        {
            var response = await _bookService.DeleteBook(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
