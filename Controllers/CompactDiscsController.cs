using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EF_lab.Models;

namespace EF_lab.Controllers
{
    public class CompactDiscsController : Controller
    {
        private readonly CompactDiscContext _context;

        public CompactDiscsController(CompactDiscContext context)
        {
            _context = context;
        }

        // GET: CompactDiscs
        public async Task<IActionResult> Index()
        {
            var compactDiscContext = _context.Discs.Include(c => c.Genre);
            return View(await compactDiscContext.ToListAsync());
        }

        // GET: CompactDiscs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compactDisc = await _context.Discs
                .Include(c => c.Genre)
                .FirstOrDefaultAsync(m => m.CompactDiscId == id);
            if (compactDisc == null)
            {
                return NotFound();
            }

            return View(compactDisc);
        }

        // GET: CompactDiscs/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName");
            return View();
        }

        // POST: CompactDiscs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompactDiscId,CompactDiscTitle,ArtistName,GenreId")] CompactDisc compactDisc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compactDisc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", compactDisc.GenreId);
            return View(compactDisc);
        }

        // GET: CompactDiscs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compactDisc = await _context.Discs.FindAsync(id);
            if (compactDisc == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", compactDisc.GenreId);
            return View(compactDisc);
        }

        // POST: CompactDiscs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompactDiscId,CompactDiscTitle,ArtistName,GenreId")] CompactDisc compactDisc)
        {
            if (id != compactDisc.CompactDiscId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compactDisc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompactDiscExists(compactDisc.CompactDiscId))
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", compactDisc.GenreId);
            return View(compactDisc);
        }

        // GET: CompactDiscs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compactDisc = await _context.Discs
                .Include(c => c.Genre)
                .FirstOrDefaultAsync(m => m.CompactDiscId == id);
            if (compactDisc == null)
            {
                return NotFound();
            }

            return View(compactDisc);
        }

        // POST: CompactDiscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compactDisc = await _context.Discs.FindAsync(id);
            _context.Discs.Remove(compactDisc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompactDiscExists(int id)
        {
            return _context.Discs.Any(e => e.CompactDiscId == id);
        }
    }
}
