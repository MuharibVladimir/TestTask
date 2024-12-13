using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService: IAuthorService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthorService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Author> GetAuthor()
        {
            var maxTitleLength = await _applicationDbContext.Books.MaxAsync(b => b.Title.Length);

            var author = await _applicationDbContext.Authors
                .Where(a => a.Books.Any(b => b.Title.Length == maxTitleLength))
                .OrderBy(a => a.Id)
                .FirstOrDefaultAsync();
            return author;
        }

        public async Task<List<Author>> GetAuthors()
        {
            var authors = await _applicationDbContext.Authors
                .Where(a => a.Books
                .Any(b => b.PublishDate.Year > 2015))
                .Where(a => a.Books
                .Count(b => b.PublishDate.Year > 2015) % 2 == 0) 
                .ToListAsync();

            return authors;
        }
    }
}
