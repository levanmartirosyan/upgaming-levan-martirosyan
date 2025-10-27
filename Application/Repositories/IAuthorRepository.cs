using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author?> GetAuthorById(int authorId);
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<Author> DeleteAuthor(int authorId);
        Task<bool> AuthorExistsByName(string authorName);
        Task<bool> AuthorExistsById(int authorId);
        Task<bool> SaveChangesAsync();
    }
}
