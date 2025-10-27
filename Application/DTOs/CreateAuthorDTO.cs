using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.DTOs
{
    public class CreateAuthorDTO
    {
        [Required(ErrorMessage = "Author name is required.")]
        public string Name { get; set; }
    }
}
