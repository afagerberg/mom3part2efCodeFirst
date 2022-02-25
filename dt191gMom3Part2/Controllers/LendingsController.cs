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
//DT191G Moment 3 del 2, av Alice Fagerberg
namespace dt191gMom3Part2.Controllers
{
    //Controller Lendings
    public class LendingsController : Controller
    {
        private readonly CollectionContext _context;

        public LendingsController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Lendings - Hämtar alla användare och lån
        public async Task<IActionResult> Index()
        {
            //Alla användare
            List<User> Users = await _context.User.ToListAsync();
            ViewBag.Users = Users;

            //Alla lån
            var collectionContext = _context.Lending.Include(l => l.CDalbum).Include(l => l.User);
            return View(await collectionContext.ToListAsync());
        }

        // GET: Lendings/Delete/5 - återlämna sida
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lending = await _context.Lending
                .Include(l => l.CDalbum)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LendingId == id);
            if (lending == null)
            {
                return NotFound();
            }

            return View(lending);
        }

        // POST: Lendings/Delete/5 -återlämnar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Återlämnar (raderar)
            var lending = await _context.Lending.FindAsync(id);
            _context.Lending.Remove(lending);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LendingExists(int id)
        {
            return _context.Lending.Any(e => e.LendingId == id);
        }
    }
}
