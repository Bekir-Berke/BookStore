using System;
using WebApi.DbOperations;
using System.Linq;
namespace WebApi.BookOperations
{
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

    }
    public class UpdateBook
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateBook(BookStoreDbContext context){
            _context = context;
        }
        public void Handle(int id){
            var book = _context.Books.SingleOrDefault(b => b.GenreId == Model.GenreId);
            if(book is null){
                throw new InvalidOperationException("kitap bulunamadı");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _context.SaveChanges();
        }
    }
}