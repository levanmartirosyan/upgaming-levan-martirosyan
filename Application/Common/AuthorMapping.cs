using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Common
{
    public static class AuthorMapping
    {
        // Entity To DTO
        public static AuthorDTO ToAuthorDTO(this Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        // Create DTO To Entity
        public static Author ToAuthorEntity(this CreateAuthorDTO dto)
        {
            return new Author
            {
                Name = dto.Name
            };
        }

        // List<Entity> To List<DTO>
        public static List<AuthorDTO> ToAuthorDTOList(this List<Author> authors)
        {
            return authors.Select(a => a.ToAuthorDTO()).ToList();
        }
    }
}
