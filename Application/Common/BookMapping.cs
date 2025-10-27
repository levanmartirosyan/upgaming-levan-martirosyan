using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Common
{
    public static class BookMapping
    {
        // Entity To DTO
        public static BookDTO ToBookDTO(this Book book)
        {
            return new BookDTO
            {
                ID = book.Id,
                Title = book.Title,
                AuthorName = book.Author?.Name ?? string.Empty, 
                PublicationYear = book.PublicationYear
            };
        }

        // Create DTO To Entity
        public static Book ToBookEntity(this CreateBookDTO dto)
        {
            return new Book
            {
                Title = dto.Title,
                AuthorID = dto.AuthorId,  
                PublicationYear = dto.PublicationYear
            };
        }

        // List<Entity> To List<DTO>
        public static List<BookDTO> ToBookDTOList(this List<Book> books)
        {
            return books.Select(b => b.ToBookDTO()).ToList();
        }
    }
}
