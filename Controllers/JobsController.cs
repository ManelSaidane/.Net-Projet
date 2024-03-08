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
    public class JobsController : Controller
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var Entreprises_id = HttpContext.Session.GetInt32("Entreprise");

            if (Entreprises_id != null) {
                var appDbContext1 = _context.Jobs.Include(j => j.Entreprise).Where(j => j.EntrepriseId == Entreprises_id);
                return View(await appDbContext1.ToListAsync());
            }

            // Filtrer les Jobs par EntrepriseId
            var appDbContext = _context.Jobs.Include(j => j.Entreprise);
                return View(await appDbContext.ToListAsync());
            
            
        }


        public async Task<IActionResult> Index2()
        {

            var appDbContext1 = _context.Jobs.ToList();
            return View(appDbContext1);

        }


        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var Job = await _context.Jobs
                .Include(j => j.Entreprise)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Job == null)
            {
                return NotFound();
            }

            return View(Job);
        }

        public async Task<IActionResult> DetailsCandidat(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var Job = await _context.Jobs
                .Include(j => j.Entreprise)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Job == null)
            {
                return NotFound();
            }

            return View(Job);
        }

        [HttpPost]
        public async Task<IActionResult> Candidate_list(int id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var l = from c in _context.Candidats
                    join ja in _context.Applications on c.Id equals ja.CandidatId
                    join jo in _context.Jobs on ja.JobId equals jo.Id
                    where jo.Id == id
                    select new Candidat_Job
                    {
                      
                        cName = c.Name,
                        cSurname = c.Surname,
                        cSkills = c.Skills,
                        cResume = c.Resume,
                        cTel = c.Tel,
                        cEmail = c.Email,
                        jaStatus = ja.Status
                    };

            if (l == null)
            {
                return NotFound();
            }

            return View(l);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatu(int id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var l = from c in _context.Candidats
                    join ja in _context.Applications on c.Id equals ja.CandidatId
                    join jo in _context.Jobs on ja.JobId equals jo.Id
                    where jo.Id == id
                    select new Candidat_Job
                    {
                        jaStatus = ja.Status
                    };

            if (l == null)
            {
                return NotFound();
            }

            return View(l);
        }
        public IActionResult LogoutEntreprise() /**/
        {
            HttpContext.Session.Remove("Entreprise"); // Remove the session variable
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Entreprise");
        }
        public IActionResult LogoutCandidat()
        {
            HttpContext.Session.Remove("Candidat"); // Remove the session variable
            return RedirectToAction("Login","Candidats");
        }
       


       



        // GET: Jobs/Create
        public IActionResult Create()
        {
           // ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "Description");
            ViewBag.Entreprise_id = HttpContext.Session.GetInt32("Entreprise");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,SkillsNeeded,Location")] Job Job)
        {
            if (HttpContext.Session.GetInt32("Entreprise") != null)
            {
                Job.EntrepriseId = (int)HttpContext.Session.GetInt32("Entreprise");
            }

            if (ModelState.IsValid)
            {
                _context.Add(Job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Job);
           
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var Job = await _context.Jobs.FindAsync(id);
            if (Job == null)
            {
                return NotFound();
            }
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "Description", Job.EntrepriseId);
            return View(Job);
        }


   




        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PostedDate,SkillsNeeded,Location,EntrepriseId")] Job Job)
        {
            if (id != Job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(Job.Id))
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
            ViewData["EntrepriseId"] = new SelectList(_context.Entreprises, "Id", "Description", Job.EntrepriseId);
            return View(Job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var Job = await _context.Jobs
                .Include(j => j.Entreprise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Job == null)
            {
                return NotFound();
            }

            return View(Job);
        }


      


        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jobs == null)
            {
                return Problem("Entity set 'AppDbContext.Jobs'  is null.");
            }
            var Job = await _context.Jobs.FindAsync(id);
            if (Job != null)
            {
                _context.Jobs.Remove(Job);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
          return (_context.Jobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}