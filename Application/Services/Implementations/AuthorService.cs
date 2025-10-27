using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Repositories;
using BookCatalog.Application.Services.Interfaces;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Services.Implementatios
{
    public class AuthorService : IAuthorService
    {
        // Private read-only field to hold a reference to the Author repository.
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // Get All Authors
        public async Task<ServiceResponse<List<AuthorDTO>>> GetAllAuthors()
        {
            // Fetch all authors from the repository (in-memory source)
            var authors = await _authorRepository.GetAllAuthors();

            // If no authors were found, return a failure response with status 404
            if (!authors.Any())
            {
                return ServiceResponse<List<AuthorDTO>>.Fail("No authors found.", 404);
            }

            // Map the Author entities to AuthorDTOs using the mapping extension method
            var authorDTOs = authors.ToAuthorDTOList();

            // Return a successful ServiceResponse with the mapped list and default status 200
            return ServiceResponse<List<AuthorDTO>>.Success(authorDTOs);
        }

        // Get Author By Id
        public async Task<ServiceResponse<AuthorDTO>> GetAuthorById(int authorId)
        {
            if (authorId <= 0)
            {
                return ServiceResponse<AuthorDTO>.Fail("Invalid author ID.", 400);
            }

            var author = await _authorRepository.GetAuthorById(authorId);

            if (author == null)
            {
                return ServiceResponse<AuthorDTO>.Fail("Author not found.", 404);
            }

            var authorDTO = author.ToAuthorDTO();

            return ServiceResponse<AuthorDTO>.Success(authorDTO);
        }

        // Author Creating
        public async Task<ServiceResponse<AuthorDTO>> CreateAuthor(CreateAuthorDTO CreateAuthorDTO)
        {
            if (CreateAuthorDTO == null)
            {
                return ServiceResponse<AuthorDTO>.Fail("Author data is required.");
            }

            if (string.IsNullOrWhiteSpace(CreateAuthorDTO.Name))
            {
                return ServiceResponse<AuthorDTO>.Fail("Name field cannot be null or empty.");
            }

            var authorExists = await _authorRepository.AuthorExistsByName(CreateAuthorDTO.Name);

            if (authorExists)
            {
                return ServiceResponse<AuthorDTO>.Fail($"Author with name - '{ CreateAuthorDTO.Name}' already exists.", 409);
            }

            var author = CreateAuthorDTO.ToAuthorEntity();

            await _authorRepository.AddAuthor(author);

            var result = await _authorRepository.SaveChangesAsync();

            if (!result)
            {
                return ServiceResponse<AuthorDTO>.Fail("Failed to add author.", 500);
            }

            var authorDTO = author.ToAuthorDTO();

            return ServiceResponse<AuthorDTO>.Success(authorDTO);
        }

        // Author Deleting
        public async Task<ServiceResponse<AuthorDTO>> DeleteAuthor(int authorId)
        {
            if (authorId <= 0)
            {
                return ServiceResponse<AuthorDTO>.Fail("Invalid author ID.", 400);
            }

            var authorExists = await _authorRepository.AuthorExistsById(authorId);

            if (!authorExists)
            {
                return ServiceResponse<AuthorDTO>.Fail($"Author with ID - '{authorId}' not found.", 404);
            }

            await _authorRepository.DeleteAuthor(authorId);

            var result = await _authorRepository.SaveChangesAsync();

            if (!result)
            {
                return ServiceResponse<AuthorDTO>.Fail("Failed to delete author.", 500);
            }

            return ServiceResponse<AuthorDTO>.Success(null);
        }

        // Author Editing
        public async Task<ServiceResponse<AuthorDTO>> UpdateAuthor(AuthorDTO authorDTO)
        {
            if (authorDTO.Id <= 0)
            {
                return ServiceResponse<AuthorDTO>.Fail("Invalid author ID.", 400);
            }

            var TrimmedAuthorName = authorDTO.Name.Trim();

            if (string.IsNullOrWhiteSpace(TrimmedAuthorName))
            {
                return ServiceResponse<AuthorDTO>.Fail("Name field cannot be null or empty.", 400);
            }

            var authorExists = await _authorRepository.AuthorExistsByName(authorDTO.Name);

            if (authorExists)
            {
                return ServiceResponse<AuthorDTO>.Fail($"Author with name - '{authorDTO.Name}' already exists", 409);
            }

            var author = await _authorRepository.GetAuthorById(authorDTO.Id);

            if (author == null)
            {
                return ServiceResponse<AuthorDTO>.Fail($"Author with ID - '{authorDTO.Id}' not found", 404);
            }

            author.Name = TrimmedAuthorName;

            _authorRepository.UpdateAuthor(author);

            var result = await _authorRepository.SaveChangesAsync();

            if (!result)
            {
                return ServiceResponse<AuthorDTO>.Fail("Failed to delete author.", 500);
            }

            var auth = author.ToAuthorDTO();

            return ServiceResponse<AuthorDTO>.Success(auth);
        }
    }
}
