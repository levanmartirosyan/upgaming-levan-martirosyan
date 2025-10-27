using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Services.Interfaces;

namespace BookCatalog.Application.Services.Implementatios
{
    public class BookService : IBookService
    {
        public Task<ServiceResponse<CreateBookDTO>> CreateBook(CreateBookDTO createBookDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> DeleteBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<BookDTO>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<BookDTO>> GetBookByAuthorId(int authorId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<BookDTO>> GetBookById(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> UpdateBook(UpdateBookDTO updateBookDTO)
        {
            throw new NotImplementedException();
        }
    }
}
