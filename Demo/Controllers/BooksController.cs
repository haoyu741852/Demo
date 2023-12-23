using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Data;
using Demo.Models;
using Demo.Mappings;
using Demo.Service.Interface;
using Demo.Service.Implement;
using Demo.Service.Models;

namespace Demo.Controllers
{
    public class BooksController : Controller
    {
        private readonly DemoContext _context;
        //ef
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(DemoContext context, IBookService bookService)
        {
            _context = context;
            //ef
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ControllerMappings>());

            this._mapper = config.CreateMapper();
            this._bookService = bookService;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchString)
        {
            var info = new BookSearchInfo { Name = searchString };

            var bookresult = this._bookService.GetList(info);
            var book = this._mapper.Map<IEnumerable<BookResultModel>, IEnumerable<Book>>(bookresult);

            return View(book);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var bookresult = this._bookService.Get(id);

            var book = this._mapper.Map<BookResultModel, Book>(bookresult);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Genre,ReleaseDate,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                var bookinfo = this._mapper.Map<Book, BookInfo>(book);

                var isSuccess = this._bookService.Insert(bookinfo);
                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(book);
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var bookresult = this._bookService.Get(id);

            var book = this._mapper.Map<BookResultModel, Book>(bookresult);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Genre,ReleaseDate,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bookresult = this._bookService.Get(id);
                    if (bookresult is null)
                    {
                        return NotFound();
                    }

                    var bookinfo = this._mapper.Map<Book, BookInfo>(book);
                    var isSuccess = this._bookService.Update(id, bookinfo);
                    if (isSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View(book);
                }
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var bookresult = this._bookService.Get(id);

            var book = this._mapper.Map<BookResultModel, Book>(bookresult);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookresult = this._bookService.Get(id);
            if (bookresult is null)
            {
                return NotFound();
            }
            this._bookService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
