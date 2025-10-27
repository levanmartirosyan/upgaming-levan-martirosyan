using BookCatalog.Domain.Entities;

namespace BookCatalog.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Exit early if data already exists (prevents duplicate seeding)
            if (context.Authors.Any() || context.Books.Any())
            {
                return;
            }

            // Create initial authors
            var authors = new Author[]
            {
                new Author { Id = 1, Name = "Robert C. Martin" },
                new Author { Id = 2, Name = "Jeffrey Richter"  }
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();

            // Create initial books linked to authors
            var books = new Book[]
            {
                new Book { Id = 1, Title = "Clean Code", AuthorID = 1, PublicationYear = 2008 },
                new Book { Id = 2, Title = "CLR via C#", AuthorID = 2, PublicationYear = 2012 },
                new Book { Id = 3, Title = "The Clean Coder", AuthorID = 1, PublicationYear = 2011 }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
