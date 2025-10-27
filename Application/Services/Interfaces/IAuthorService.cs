using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;

namespace BookCatalog.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<ServiceResponse<List<AuthorDTO>>> GetAllAuthors();
        Task<ServiceResponse<AuthorDTO>> GetAuthorById(int authorId);
        Task<ServiceResponse<AuthorDTO>> CreateAuthor(CreateAuthorDTO authorDTO);
        Task<ServiceResponse<AuthorDTO>> UpdateAuthor(AuthorDTO authorDTO);
        Task<ServiceResponse<AuthorDTO>> DeleteAuthor(int authorId);
    }
}
