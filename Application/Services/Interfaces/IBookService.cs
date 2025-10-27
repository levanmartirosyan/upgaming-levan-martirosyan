using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;

namespace BookCatalog.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<ServiceResponse<BookDTO>> GetAllBooks();
        Task<ServiceResponse<BookDTO>> GetBookById(int bookId);
        Task<ServiceResponse<BookDTO>> GetBookByAuthorId(int authorId);
        Task<ServiceResponse<CreateBookDTO>> CreateBook(CreateBookDTO createBookDTO);
        Task<ServiceResponse<bool>> UpdateBook(UpdateBookDTO updateBookDTO);
        Task<ServiceResponse<bool>> DeleteBook(int bookId);
    }
}
