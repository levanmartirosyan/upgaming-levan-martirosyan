using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Common
{
    public static class AuthorMapping
    {
        public static AuthorDTO ToAuthorDTO(this Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public static Author ToAuthorEntity(this CreateAuthorDTO dto)
        {
            return new Author
            {
                Name = dto.Name
            };
        }

        public static List<AuthorDTO> ToAuthorDTOList(this List<Author> authors)
        {
            return authors.Select(a => a.ToAuthorDTO()).ToList();
        }
    }
}
