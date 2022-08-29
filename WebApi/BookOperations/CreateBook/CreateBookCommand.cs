using WebApi.DbOperations;
using System.Linq;
using System;
namespace WebApi.BookOperations
{
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
    public class CreateBook{
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public CreateBook(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(){
            var book = _context.Books.SingleOrDefault(b => b.Title == Model.Title);
            if(book is not null){
                throw new InvalidOperationException("kitap zaten var");
            }
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
}