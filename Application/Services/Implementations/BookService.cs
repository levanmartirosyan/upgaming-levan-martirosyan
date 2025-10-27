using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Repositories;
using BookCatalog.Application.Services.Interfaces;
using BookCatalog.Domain.Entities;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookCatalog.Application.Services.Implementatios
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorService _authorService;

        public BookService(IBookRepository bookRepository, IAuthorService authorService)
        {
            _bookRepository = bookRepository;
            _authorService = authorService;
        }

        public async Task<ServiceResponse<List<BookDTO>>> GetAllBooks(BookFilterParams filterParams)
        {
            var books = await _bookRepository.GetAllBooks(filterParams);

            if (!books.Any())
            {
                return ServiceResponse<List<BookDTO>>.Fail("No books found", 404);
            }
            
            var bookDTO = books.ToBookDTOList();

            return ServiceResponse<List<BookDTO>>.Success(bookDTO);
        }

        public async Task<ServiceResponse<BookDTO>> GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                return ServiceResponse<BookDTO>.Fail("Invalid book ID.", 400);
            }

            var book = await _bookRepository.GetBookById(bookId);

            if (book == null)
            {
                return ServiceResponse<BookDTO>.Fail("Book not found.", 404);
            }

            var bookDTO = book.ToBookDTO();

            return ServiceResponse<BookDTO>.Success(bookDTO);
        }

        public async Task<ServiceResponse<List<BookDTO>>> GetBooksByAuthorId(int authorId)
        {
            var author = await _authorService.GetAuthorById(authorId);

            if (!author.IsSuccess)
            {
                return ServiceResponse<List<BookDTO>>.Fail("Author not found.", 404);
            }

            var books = await _bookRepository.GetBooksByAuthor(authorId);

            if (!books.Any())
            {
                return ServiceResponse<List<BookDTO>>.Fail($"No books found for author with id - '{authorId}'.", 404);
            }

            var bookDTO = books.ToBookDTOList();

            return ServiceResponse<List<BookDTO>>.Success(bookDTO);
        }

        public async Task<ServiceResponse<BookDTO>> CreateBook(CreateBookDTO createBookDTO)
        {
            if (createBookDTO == null)
            {
                return ServiceResponse<BookDTO>.Fail("Book data can't be null.", 400);
            }

            var bookExists = await _bookRepository.BookExists(createBookDTO.Title);

            if (bookExists)
            {
                return ServiceResponse<BookDTO>.Fail($"Book with title - '{createBookDTO.Title}' already exists.", 409);
            }

            var book = createBookDTO.ToBookEntity();

            await  _bookRepository.AddBook(book);

            var result = await _bookRepository.SaveChangesAsync();

            if (!result)
            {
                return ServiceResponse<BookDTO>.Fail("Failed to add book.", 500);
            }

            var bookDTO = book.ToBookDTO();

            return ServiceResponse<BookDTO>.Success(bookDTO, 201);
        }

        public async Task<ServiceResponse<BookDTO>> UpdateBook(UpdateBookDTO updateBookDTO)
        {
            if (updateBookDTO == null)
            {
                return ServiceResponse<BookDTO>.Fail("Book data can't be null.", 400);
            }

            if (updateBookDTO.Id <= 0)
            {
                return ServiceResponse<BookDTO>.Fail("Invalid book ID.", 400);
            }

            var book = await _bookRepository.GetBookById(updateBookDTO.Id);

            if (book == null)
            {
                return ServiceResponse<BookDTO>.Fail("Book not found.", 404);
            }

            book.AuthorID = updateBookDTO.AuthorID;

            var author = await _authorService.GetAuthorById(book.AuthorID);

            if (!author.IsSuccess)
            {
                return ServiceResponse<BookDTO>.Fail($"Author with ID - '{book.AuthorID}' not found.", 404);
            }

            book.Title = updateBookDTO.Title;
            book.PublicationYear = updateBookDTO.PublicationYear;

            _bookRepository.UpdateBook(book);

            var result = await _bookRepository.SaveChangesAsync();

            if (!result)
            {
                return ServiceResponse<BookDTO>.Fail("Failed to update book.", 500);
            }

            var bookDTO = book.ToBookDTO();
            return ServiceResponse<BookDTO>.Success(bookDTO);
        }

        public async Task<ServiceResponse<BookDTO>> DeleteBook(int bookId)
        {
            if (bookId <= 0)
            {
                return ServiceResponse<BookDTO>.Fail("Invalid book ID.", 400);
            }

            var book = await _bookRepository.GetBookById(bookId);

            if (book == null)
            {
                return ServiceResponse<BookDTO>.Fail("Book not found.", 404);
            }

            await _bookRepository.DeleteBook(bookId);

            var result = await _bookRepository.SaveChangesAsync();

            if (!result)
            {
                return ServiceResponse<BookDTO>.Fail("Failed to delete book.", 500);
            }

            return ServiceResponse<BookDTO>.Success(null);
        }
    }
}
