using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book{
                        // Id = 1,
                        Title = "Harry Potter and the Philosopher's Stone",
                        GenreId = 1,
                        PublishDate = new DateTime(1997, 7, 1),
                        PageCount = 223
                    },
                    new Book{
                        // Id = 2,
                        Title = "Harry Potter and the Chamber of Secrets",
                        GenreId = 1,
                        PublishDate = new DateTime(1998, 7, 1),
                        PageCount = 223
                    },
                    new Book{
                        // Id = 3,
                        Title = "Lean Startup",
                        GenreId = 2,
                        PublishDate = new DateTime(1999, 7, 1),
                        PageCount = 231
                    }
                );
                context.SaveChanges();
            }
        }
    }
}