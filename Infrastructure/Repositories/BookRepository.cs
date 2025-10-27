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

        // Retrieve all books from the database (optionally filtered and sorted)
        public async Task<List<Book>> GetAllBooks(BookFilterParams filterParams)
        {
            // Start the query and include related Author data
            var books = _dbContext.Books
                .Include(b => b.Author)
                .AsQueryable();

            // Apply sorting based on the SortBy parameter
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

            // Apply filtering by publication year if provided
            if (filterParams.PublicationYear.HasValue && filterParams.PublicationYear.Value > 0)
            {
                books = books.Where(b => b.PublicationYear == filterParams.PublicationYear.Value);
            }

            // Execute the query and return the result as a list
            return await books.ToListAsync();
        }

        // Retrieve a single book by ID (including author details)
        public Task<Book?> GetBookById(int id)
        {
            return _dbContext.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        // Retrieve all books written by a specific author
        public async Task<List<Book>> GetBooksByAuthor(int authorId)
        {
            return await _dbContext.Books
                .Where(b => b.AuthorID == authorId)
                .Include(b => b.Author)
                .ToListAsync();
        }

        // Add a new book to the database context
        public async Task AddBook(Book book)
        {
            await _dbContext.AddAsync(book);
        }

        // Delete a book by ID
        public async Task DeleteBook(int id)
        {
            var author = await _dbContext.Books.FindAsync(id);

            _dbContext.Books.Remove(author);
        }

        // Update an existing book
        public async Task UpdateBook(Book book)
        {
            _dbContext.Books.Update(book);
        }

        // Check if a book with the given title already exists (case-insensitive)
        public async Task<bool> BookExists(string bookTitle)
        {
            return await _dbContext.Books.AnyAsync(b => b.Title.ToLower() == bookTitle.ToLower());
        }

        // Save pending changes to the database
        public async Task<bool> SaveChangesAsync()
        {
            // Returns true if one or more rows were affected
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
