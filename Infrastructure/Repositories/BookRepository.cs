using BookCatalog.Application.DTOs;
using BookCatalog.Application.Repositories;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookCatalog.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Book>> GetAllBooks(BookFilterParams filterParams)
        {
            var books = _dbContext.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.SortBy))
            {
                switch (filterParams.SortBy.ToLower())
                {
                    case "title_asc":
                        books = books.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        books = books.OrderByDescending(b => b.Title);
                        break;
                    case "year_asc":
                        books = books.OrderBy(b => b.PublicationYear);
                        break;
                    case "year_desc":
                        books = books.OrderByDescending(b => b.PublicationYear);
                        break;
                    default:
                        break;
                }
            }

            if (filterParams.PublicationYear.HasValue && filterParams.PublicationYear.Value > 0)
            {
                books = books.Where(b => b.PublicationYear == filterParams.PublicationYear.Value);
            }

            return await books.ToListAsync();
        }

        public Task<Book?> GetBookById(int id)
        {
            return _dbContext.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetBooksByAuthor(int authorId)
        {
            return await _dbContext.Books
                .Where(b => b.AuthorID == authorId)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task AddBook(Book book)
        {
            await _dbContext.AddAsync(book);
        }

        public async Task DeleteBook(int id)
        {
            var author = await _dbContext.Books.FindAsync(id);

            _dbContext.Books.Remove(author);
        }

        public async Task UpdateBook(Book book)
        {
            _dbContext.Books.Update(book);
        }

        public async Task<bool> BookExists(string bookTitle)
        {
            return await _dbContext.Books.AnyAsync(b => b.Title.ToLower() == bookTitle.ToLower());
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
