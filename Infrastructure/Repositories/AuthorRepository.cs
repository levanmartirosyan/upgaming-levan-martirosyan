using BookCatalog.Application.Repositories;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        // Retrieve all authors from the database
        public async Task<List<Author>> GetAllAuthors()
        {
            // Fetch all authors as a list (asynchronous)
            return await _dbContext.Authors.ToListAsync();
        }

        // Retrieve a single author by ID
        public async Task<Author> GetAuthorById(int authorId)
        {
            return await _dbContext.Authors.FindAsync(authorId);
        }

        // Add a new author to the database
        public async Task<Author> AddAuthor(Author author)
        {
            var result = await _dbContext.Authors.AddAsync(author);

            return result.Entity;
        }

        // Update an existing author record
        public async Task<Author> UpdateAuthor(Author author)
        {
            _dbContext.Authors.Update(author);

            return author;
        }

        // Delete an author by ID
        public async Task<Author> DeleteAuthor(int authorId)
        {
            var author = await _dbContext.Authors.FindAsync(authorId);

            _dbContext.Authors.Remove(author);

            return author;
        }

        // Check if an author exists by name (case-insensitive)
        public async Task<bool> AuthorExistsByName(string authorName)
        { 
            return await _dbContext.Authors.AnyAsync(a => a.Name.ToLower() == authorName.ToLower());
        }

        // Check if an author exists by ID
        public async Task<bool> AuthorExistsById(int authorId)
        { 
            return await _dbContext.Authors.AnyAsync(a => a.Id == authorId);
        }

        // Save pending changes to the database
        public async Task<bool> SaveChangesAsync()
        {
            // Returns true if one or more rows were affected
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
