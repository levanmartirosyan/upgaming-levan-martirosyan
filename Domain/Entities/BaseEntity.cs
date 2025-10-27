using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain.Entities
{
    public class BaseEntity
    {
        [Key] public int Id { get; set; }
    }
}
