using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;

namespace BookCatalog.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<ServiceResponse<List<BookDTO>>> GetAllBooks(BookFilterParams filterParams);
        Task<ServiceResponse<BookDTO>> GetBookById(int bookId);
        Task<ServiceResponse<List<BookDTO>>> GetBooksByAuthorId(int authorId);
        Task<ServiceResponse<BookDTO>> CreateBook(CreateBookDTO createBookDTO);
        Task<ServiceResponse<BookDTO>> UpdateBook(UpdateBookDTO updateBookDTO);
        Task<ServiceResponse<BookDTO>> DeleteBook(int bookId);
    }
}
