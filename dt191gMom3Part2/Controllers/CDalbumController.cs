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
//DT191G Moment 3 del 2, Av Alice Fagerberg
namespace dt191gMom3Part2.Controllers
{
    //Controller CD album
    public class CDalbumController : Controller
    {
        private readonly CollectionContext _context;

        public CDalbumController(CollectionContext context)
        {
            _context = context;
        }

        // GET: CDalbum/Index/5 - Hämta specifik cd
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cDalbum = await _context.CDalbum
                .Include(c => c.Artist).Include(c => c.Lending).Include(c => c.Lending.User).FirstOrDefaultAsync(m => m.AlbumId == id);
            if (cDalbum == null)
            {
                return NotFound();
            }

            //CD albumet
            ViewBag.AlbumID = cDalbum.AlbumId;
            ViewBag.cdName = cDalbum.CdName;
            ViewBag.Artist = cDalbum.Artist.ArtistName;
            ViewBag.Year = cDalbum.ReleaseYear;

            //För lån av CD albumet
            Lending lending = cDalbum.Lending;
            ViewBag.lending = lending;
            ViewBag.lendingDate = cDalbum.Lending?.LendingDate.ToString("d MMMM yyyy");
            ViewBag.user = cDalbum.Lending?.User.Name;

            //I selectlista och input formulär
            ViewData["AlbumId"] = cDalbum.AlbumId;
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name");
            

            return View();
        }

        //POST: CDalbum/Index (Låna ut)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("LendingId,LendingDate,AlbumId,UserId")] Lending lending)
        {
            //Låna ut
            if (ModelState.IsValid)
            {
                _context.Add(lending);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = (_context.CDalbum, "AlbumId", "CdName", lending.AlbumId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", lending.UserId);
            return View(lending);
        }

        // GET: CDalbum/AddCD
        public IActionResult AddCD()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName");
            return View();
        }

        // POST: CDalbum/AddCD

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCD([Bind("AlbumId,CdName,ReleaseYear,ArtistId")] CDalbum cDalbum)
        {
            //Lägg till cd
            if (ModelState.IsValid)
            {
                _context.Add(cDalbum);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName", cDalbum.ArtistId);
            return View(cDalbum);
        }

        // GET: CDalbum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cDalbum = await _context.CDalbum.FindAsync(id);
            if (cDalbum == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName", cDalbum.ArtistId);
            return View(cDalbum);
        }

        // POST: CDalbum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumId,CdName,ReleaseYear,ArtistId")] CDalbum cDalbum)
        {
            if (id != cDalbum.AlbumId)
            {
                return NotFound();
            }
            //Ändra cd
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cDalbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CDalbumExists(cDalbum.AlbumId))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName", cDalbum.ArtistId);
            return View(cDalbum);
        }

        // GET: CDalbum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cDalbum = await _context.CDalbum
                .Include(c => c.Artist)
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (cDalbum == null)
            {
                return NotFound();
            }

            return View(cDalbum);
        }

        // POST: CDalbum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //radera cd
            var cDalbum = await _context.CDalbum.FindAsync(id);
            _context.CDalbum.Remove(cDalbum);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool CDalbumExists(int id)
        {
            return _context.CDalbum.Any(e => e.AlbumId == id);
        }
    }
}
