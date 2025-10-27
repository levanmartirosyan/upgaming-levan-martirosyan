using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks(BookFilterParams filterParams);
        Task<Book?> GetBookById(int id);
        Task<List<Book>> GetBooksByAuthor(int authorId);
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
        Task<bool> BookExists(string bookTitle);
        Task<bool> SaveChangesAsync();
    }
}
