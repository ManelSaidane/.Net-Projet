using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetRec.Models;


namespace ProjetRec.Controllers
{
    public class CandidatsController : Controller
    {
        private readonly AppDbContext _context;

        public CandidatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Candidats
        // GET: Candidats
        public async Task<IActionResult> Index()
        {
            int? candidatId = HttpContext.Session.GetInt32("Candidat");
            if (candidatId == null)
            {
                // Handle the case where there is no logged-in user
                return RedirectToAction("Login", "Candidats");
            }

            var candidat = await _context.Candidats
                .Include(c => c.Application)
                .FirstOrDefaultAsync(c => c.Id == candidatId);

            if (candidat == null)
            {
                // Handle the case where the user is not found
                return NotFound();
            }

            // Pass the logged-in Candidat to the view
            return View(new List<Candidat> { candidat });
        }




        // GET: Candidats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Candidats == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidat == null)
            {
                return NotFound();
            }

            return View(candidat);
        }


      

        // GET: Candidats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,Tel,Password,Skills,Resume")] Candidat candidat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidat);
        }

        // GET: Candidats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Candidats == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }
            return View(candidat);
        }

        // POST: Candidats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Tel,Password,DateNaiss,SkillsNeeded,Experience,Education")] Candidat candidat)
        {
            if (id != candidat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatExists(candidat.Id))
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
            return View(candidat);
        }

        // GET: Candidats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Candidats == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidat == null)
            {
                return NotFound();
            }

            return View(candidat);
        }

        // POST: Candidats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Candidats == null)
            {
                return Problem("Entity set 'AppDbContext.Candidats'  is null.");
            }
            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat != null)
            {
                _context.Candidats.Remove(candidat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // Add this action for displaying the login form

        public async Task<IActionResult> Index2()
        {
            var Entreprises_id = HttpContext.Session.GetInt32("Entreprise");

            var appDbContext1 = _context.Jobs.ToList();
            return View(appDbContext1);

        }












        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var candidat = _context.Candidats.SingleOrDefault(u => u.Email == email && u.Password == password);
            if (candidat != null)
            {
                HttpContext.Session.SetInt32("Candidat", candidat.Id);
                return RedirectToAction("Index2", "Jobs");
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }
        private bool CandidatExists(int id)
        {
          return (_context.Candidats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
