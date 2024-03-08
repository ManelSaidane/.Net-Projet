using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetRec.Models;

namespace ProjetRec.Controllers
{
    public class EntrepriseController : Controller
    {
        private readonly AppDbContext _context;

        public EntrepriseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Entreprises
        public async Task<IActionResult> Index()
        {
              return _context.Entreprises != null ? 
                          View(await _context.Entreprises.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Entreprises'  is null.");
        }

        // GET: Entreprises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entreprises == null)
            {
                return NotFound();
            }

            var Entreprise = await _context.Entreprises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Entreprise == null)
            {
                return NotFound();
            }

            return View(Entreprise);
        }

        // GET: Entreprises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entreprises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Tel,Location,Password,Description")] Entreprise Entreprise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Entreprise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Entreprise);
        }

        // GET: Entreprises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entreprises == null)
            {
                return NotFound();
            }

            var Entreprise = await _context.Entreprises.FindAsync(id);
            if (Entreprise == null)
            {
                return NotFound();
            }
            return View(Entreprise);
        }

        // POST: Entreprises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Tel,Location,Password,Description")] Entreprise Entreprise)
        {
            if (id != Entreprise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Entreprise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrepriseExists(Entreprise.Id))
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
            return View(Entreprise);
        }

        // GET: Entreprises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entreprises == null)
            {
                return NotFound();
            }

            var Entreprise = await _context.Entreprises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Entreprise == null)
            {
                return NotFound();
            }

            return View(Entreprise);
        }

        // POST: Entreprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entreprises == null)
            {
                return Problem("Entity set 'AppDbContext.Entreprises'  is null.");
            }
            var Entreprise = await _context.Entreprises.FindAsync(id);
            if (Entreprise != null)
            {
                _context.Entreprises.Remove(Entreprise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var Entreprise = _context.Entreprises.SingleOrDefault(u => u.Email == email && u.Password == password);
            if (Entreprise != null)
            {
                HttpContext.Session.SetInt32("Entreprise", Entreprise.Id);
                return RedirectToAction("Index", "Jobs");
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }
   
        private bool EntrepriseExists(int id)
        {
          return (_context.Entreprises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
