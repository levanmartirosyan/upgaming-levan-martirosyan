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

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int authorId)
        {
            return await _dbContext.Authors.FindAsync(authorId);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            var result = await _dbContext.Authors.AddAsync(author);

            return result.Entity;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            _dbContext.Authors.Update(author);

            return author;
        }

        public async Task<Author> DeleteAuthor(int authorId)
        {
            var author = await _dbContext.Authors.FindAsync(authorId);

            _dbContext.Authors.Remove(author);

            return author;
        }

        public async Task<bool> AuthorExistsByName(string authorName)
        { 
            return await _dbContext.Authors.AnyAsync(a => a.Name.ToLower() == authorName.ToLower());
        }

        public async Task<bool> AuthorExistsById(int authorId)
        { 
            return await _dbContext.Authors.AnyAsync(a => a.Id == authorId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
