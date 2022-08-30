using System;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Common;
using System.Linq;
namespace WebApi.BookOperations
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBookByIdQuery(BookStoreDbContext context)
        {
            _context = context;
        }
        public BookViewModel Handle(int id)
        {
            var book = _context.Books.Where(b => b.Id == id).FirstOrDefault();
            if(book == null)
            {
                throw new Exception("Aradığınız kitap bulunamadı");
            }
            var bookModel = new BookViewModel(){
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd-mm-yyyy"),
                PageCount = book.PageCount
            };
            return bookModel;
        }
    }
    public class BookViewModel
    {
         public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}