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
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _context;

        public ApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Applications.Include(j => j.Candidat).Include(j => j.Job);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var Application = await _context.Applications
                .Include(j => j.Candidat)
                .Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Application == null)
            {
                return NotFound();
            }

            return View(Application);
        }

        // GET: Applications/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int JobId, Application Application)
        {
            if (HttpContext.Session.GetInt32("Candidat") != null)
            {
                Application.CandidatId = (int)HttpContext.Session.GetInt32("Candidat");
                
            }
            
            Application.JobId = JobId;
            Application.Status = "Pending";
           // if (ModelState.IsValid)
           // {
                _context.Add(Application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}

           // return BadRequest(new { message = "error" });
        }


        







        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var Application = await _context.Applications.FindAsync(id);
            if (Application == null)
            {
                return NotFound();
            }
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "Id", "Education", Application.CandidatId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Description", Application.JobId);
            return View(Application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,CandidatId,JobId")] Application Application)
        {
            if (id != Application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(Application.Id))
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
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "Id", "Education", Application.CandidatId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Description", Application.JobId);
            return View(Application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var Application = await _context.Applications
                .Include(j => j.Candidat)
                .Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Application == null)
            {
                return NotFound();
            }

            return View(Application);
        }



      

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'AppDbContext.Applications'  is null.");
            }
            var Application = await _context.Applications.FindAsync(id);
            if (Application != null)
            {
                _context.Applications.Remove(Application);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
          return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
