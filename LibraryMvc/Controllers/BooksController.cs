using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMvc.Data;
using LibraryMvc.Models;
using Humanizer.Localisation;

namespace LibraryMvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryMvcContext _context;

        public BooksController(LibraryMvcContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var libraryMvcContext = _context.Book.Include(b => b.Set).Include(b => b.Shelf);
            return View(await libraryMvcContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Set)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["genre"] = new SelectList(_context.Set<Library>(), "genre", "genre");
            return View();
        }

        public IActionResult CreateSet()
        {
            return RedirectToAction("Create", "Sets");
        }

        public IActionResult CreateToSet(int id, string genre)
        {
            ViewData["genre"] = new SelectList(_context.Set<Library>().Where(l => l.genre == genre), "genre", "genre");
            ViewData["SetId"] = new SelectList(_context.Set<Set>().Where(s => s.Id == id), "Id", "Name");
            return View();
        }

        //need to build view to this action
        public IActionResult AddToShelf(int id, int setId, string genre)
        {
            ViewBag.Confirm = false;
            Book CorrentBook = _context.Book.FirstOrDefault(b => b.Id == id);
            ViewData["ShelfIds"] = new SelectList(_context.Set<Shelf>()
                .Include(s => s.Library)
                .Where(s => s.Library.genre == genre),
                "Id", "Id");
            return View(CorrentBook);
        }


        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Hight,Width,genre,ShelfId,SetId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SetId"] = new SelectList(_context.Set<Set>(), "Id", "Id", book.SetId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", book.ShelfId);
            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToShelf(Book book)
        {
            if (ModelState.IsValid)
            {
                var shelf = _context.Set<Shelf>().Include(s => s.Books).FirstOrDefault(s => s.Id == book.ShelfId);
                if (book.SetId == null)
                {
                    if (book.Hight < shelf.Hight && book.Width <= shelf.FreeSpace)
                    {
                        _context.Update(book);
                        await _context.SaveChangesAsync();
                        if (book.Hight <= shelf.Hight - 10)
                        {
                            ViewBag.Message = "המדף הרבה יותר גבוה מהספר";
                            ViewBag.Confirm = true;
                        }
                        else
                            return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string problem = book.Hight > shelf.Hight && book.Width >= shelf.FreeSpace ? "הגובה והרוחב לא מתאימים"
                            : book.Hight > shelf.Hight ? "הגובה לא מתאים" : "הרוחב לא מתאים";
                        ViewBag.Message = $"לא ניתן להכניס ספר למדף כי {problem}";

                        //ViewData["ShelfIds"] = new SelectList(_context.Set<Shelf>()
                        //    .Include(s => s.Library)
                        //    .Where(s => s.Library.genre == book.genre),
                        //    "Id", "Id");
                        //return View(book);
                    }
                }
                else
                {
                    Set set = _context.Set.Include(s => s.Books).FirstOrDefault(s => s.Id == book.SetId)!;
                    if (set.Hight < shelf.Hight && set.Width <= shelf.FreeSpace)
                    {

                        foreach (var b in set.Books)
                        {
                            b.ShelfId = book.ShelfId;
                            _context.Update(b);
                            await _context.SaveChangesAsync();
                        }
                        if (set.Hight <= shelf.Hight - 10)
                        {
                            ViewBag.Message = "המדף הרבה יותר גבוה מהספר";
                            ViewBag.Confirm = true;
                        }
                        else
                            return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string problem = set.Hight > shelf.Hight && set.Width >= shelf.FreeSpace ? "הגובה והרוחב לא מתאימים"
                                : set.Hight > shelf.Hight ? "הגובה לא מתאים" : "הרוחב לא מתאים";
                        ViewBag.Message = $"לא ניתן להכניס סט למדף כי {problem}";

                        //ViewData["ShelfIds"] = new SelectList(_context.Set<Shelf>()
                        //    .Include(s => s.Library)
                        //    .Where(s => s.Library.genre == book.genre),
                        //    "Id", "Id");
                        //return View(book);
                    }

                }
            }
            ViewData["ShelfIds"] = new SelectList(_context.Set<Shelf>()
                .Include(s => s.Library)
                .Where(s => s.Library.genre == book.genre),
                "Id", "Id");
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["SetId"] = new SelectList(_context.Set<Set>(), "Id", "Id", book.SetId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", book.ShelfId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Hight,Width,genre,ShelfId,SetId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SetId"] = new SelectList(_context.Set<Set>(), "Id", "Id", book.SetId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", book.ShelfId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Set)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var book = await _context.Book.Include(b => b.Set).ThenInclude(s => s.Books).FirstOrDefaultAsync(m => m.Id == id);
            if (book != null)
            {
                if (book.Set != null && book.Set.Books.Count == 1)
                {
                    _context.Set.Remove(book.Set);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
