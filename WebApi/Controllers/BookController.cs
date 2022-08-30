using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.BookOperations;
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
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById (int id){
            GetBookByIdQuery query = new GetBookByIdQuery(_context);
            try
            {
                var result = query.Handle(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddBook ([FromBody] CreateBookModel model){
            CreateBook command = new CreateBook(_context);
            try
            {
                command.Model = model;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel model){
            UpdateBook command = new UpdateBook(_context);
            try
            {
                command.Model = model;
                command.Handle(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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