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
    // Controller Home
    public class HomeController : Controller
    {
        private readonly CollectionContext _context;

        public HomeController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Home - hämtar alla cd album och artister
        public async Task<IActionResult> Index(string searchString)

        {
            //Artistlista
            var artists = await _context.Artist.ToListAsync();
            ViewBag.AllArtists = artists;

            //Alla CD album
            var collectionContext = from c in _context.CDalbum.Include(c => c.Artist).Include(c => c.Lending)
                                    select c;

            if(!String.IsNullOrEmpty(searchString))
            {
                collectionContext = collectionContext.Where(s => s.CdName!.Contains(searchString));
            }

            return View(await collectionContext.ToListAsync());
        }
    }
}
