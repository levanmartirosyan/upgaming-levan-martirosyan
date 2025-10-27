using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.DTOs
{
    public class CreateBookDTO
    {
        [Required(ErrorMessage = "Book title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author ID is required.")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Publication year is required.")]
        [Range(0, 2025, ErrorMessage = "Publication year must be between 0 and 2025")]
        public int PublicationYear { get; set; }

    }
}
