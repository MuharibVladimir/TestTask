using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public sealed class BookService: IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Book> GetBook()
        {
            var book =  await _applicationDbContext.Books
                .OrderByDescending(b => b.QuantityPublished * b.Price)
                .FirstOrDefaultAsync();

            return book;
        }

        public async Task<List<Book>> GetBooks()
        {
            var books = await _applicationDbContext.Books
                .Where(b => b.Title.Contains("Red") && b.PublishDate > new DateTime(2012, 5, 25))
                .ToListAsync();

            return books;
        }
    }
}
