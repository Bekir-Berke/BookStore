using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using WebApi.DbOperations;
namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(b => b.Id).ToList<Book>();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetById (int id){
            var book = _context.Books.Where(b => b.Id == id).SingleOrDefault();
            return book;
        }
        [HttpPost]
        public IActionResult AddBook ([FromBody] Book newBook){
            var book = _context.Books.SingleOrDefault(b => b.Title == newBook.Title);
            if(book is not null){
                return BadRequest();
            }
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updateBook){
            var book = _context.Books.SingleOrDefault(b => b.Id == updateBook.Id);
            if(book is null){
                return NotFound();
            }
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id){
            var book = _context.Books.Where(b => b.Id == id).SingleOrDefault();
            if(book is null){
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}