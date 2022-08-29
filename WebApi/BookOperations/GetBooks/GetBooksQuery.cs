using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.BookOperations
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBooksQuery(BookStoreDbContext context)
        {
            _context = context;
        }
        public List<BooksViewModel> Handle(){
            var booklist = _context.Books.OrderBy(b => b.Id).ToList<Book>();
            List<BooksViewModel> bookViewList = new List<BooksViewModel>();
            foreach (var book in booklist)
            {
                bookViewList.Add(new BooksViewModel(){
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd-mm-yyyy"),
                    PageCount = book.PageCount
                });
            }
            return bookViewList;
        }
    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}