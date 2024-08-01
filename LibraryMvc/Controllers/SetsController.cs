using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMvc.Data;
using LibraryMvc.Models;
using LibraryMvc.ViewModels;

namespace LibraryMvc.Controllers
{
    public class SetsController : Controller
    {
        private readonly LibraryMvcContext _context;

        public SetsController(LibraryMvcContext context)
        {
            _context = context;
        }

        // GET: Sets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Set.Include(s => s.Books).ToListAsync());
        }

        // GET: Sets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Set
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // GET: Sets/Create
        public IActionResult Create()
        {
            ViewData["Genres"] = new SelectList(_context.Set<Library>(), "genre", "genre");
            return View();
        }

        // GET: Sets/Create
        public IActionResult CreateBook(int id, string genre)
        {
            return RedirectToAction("CreateToSet", "Books", new { id = id, genre = genre });
        }

        // POST: Sets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SetBooks setviewmodel)
        {
            foreach (var item in setviewmodel.Books)
            {
                item.genre = setviewmodel.Set.Genre;
            }

            if (ModelState.IsValid)
            {
                _context.Add(setviewmodel.Set);
                await _context.SaveChangesAsync();
                foreach (Book book in setviewmodel.Books)
                {
                    book.genre = setviewmodel.Set.Genre;
                    book.SetId = setviewmodel.Set.Id;
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //public async Task<IActionResult> Create([Bind("Id,Name,Genre")] Set @set)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(@set);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(@set);
        //}

        // GET: Sets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Set.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }
            return View(@set);
        }

        // POST: Sets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Set @set)
        {
            if (id != @set.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@set);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetExists(@set.Id))
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
            return View(@set);
        }

        // GET: Sets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Set
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // POST: Sets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @set = await _context.Set.Include(s => s.Books).FirstOrDefaultAsync(m => m.Id == id);
            if (@set != null)
            {
                _context.Set.Remove(@set);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetExists(int id)
        {
            return _context.Set.Any(e => e.Id == id);
        }
    }
}
