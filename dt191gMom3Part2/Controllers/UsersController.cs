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
    // Controller User
    public class UsersController : Controller
    {
        private readonly CollectionContext _context;

        public UsersController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Users specifik by id
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.Include(m => m.Loans)
                .SingleOrDefaultAsync(m => m.UserId == id);

            List<CDalbum> cds = await _context.CDalbum.ToListAsync();
            ViewBag.CDs = cds;
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        // GET: Users/AddUser
        public IActionResult AddUser()
        {
            return View();
        }

        // POST: Users/AddUser

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser([Bind("UserId,Name")] User user)
        {
            //Lägger till anv vid giltig input
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lendings");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }
            //Ändrar snv vid giltig input
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Users", new { id = user.UserId });
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Raderar anv
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Lendings");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
