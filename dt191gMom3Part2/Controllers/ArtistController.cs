#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dt191gMom3Part2.Data;
using dt191gMom3Part2.Models;

//DT191G Moment 3 del 2
namespace dt191gMom3Part2.Controllers
{
    // Controller Artist
    public class ArtistController : Controller
    {
        private readonly CollectionContext _context;

        public ArtistController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Artist index med detaljer/id
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Kontext för artist
            var artist = await _context.Artist.Include(m => m.CDalbums).SingleOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }


        // GET: Artist/AddArtist
        public IActionResult AddArtist()
        {
            return View();
        }

        // POST: Artist/AddArtist

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArtist([Bind("ArtistId,ArtistName")] Artist artist)
        {   
            //Lägg till vid giltig input
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(artist);
        }

        // GET: Artist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artist/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ArtistId,ArtistName")] Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return NotFound();
            }
            //Ändra vid giltig input
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.ArtistId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(artist);
        }

        // GET: Artist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .FirstOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            //Ta bort utifrån ID
            var artist = await _context.Artist.FindAsync(id);
            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool ArtistExists(int? id)
        {
            return _context.Artist.Any(e => e.ArtistId == id);
        }
    }
}
