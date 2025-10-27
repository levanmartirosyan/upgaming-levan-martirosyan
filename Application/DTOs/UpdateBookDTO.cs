using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.DTOs
{
    public class UpdateBookDTO
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        public string Title { get; set; }

        public int AuthorID { get; set; }

        [Range(0, 2025, ErrorMessage = "Publication year must be between 0 and 2025.")]
        public int PublicationYear { get; set; }
    }
}
