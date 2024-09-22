using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservation.Controllers
{
    public class DepartementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departement/Index
        public async Task<IActionResult> Index()
        {
            var departements = await _context.Departements.Include(d => d.Admin).ToListAsync();
            return View(departements);
        }

        public IActionResult Create()
        {
            ViewBag.Admins = new SelectList(_context.Admins.ToList(), "Id", "Nom");
            return View(new DepartementViewModel());
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Admins = new SelectList(_context.Admins.ToList(), "Id", "Nom");
                return View(model);
            }
            var departement = new Departement
            {
                NomDept = model.NomDept,
                NomSalle = model.NomSalle,
                TypeDept = model.TypeDept,
                Budget = model.Budget,
                AdminId = model.AdminId
            };
            _context.Departements.Add(departement);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var departement = await _context.Departements.FindAsync(id);
            if (departement == null)
                return NotFound();

            var model = new DepartementViewModel
            {
                Id = departement.Id,
                NomDept = departement.NomDept,
                NomSalle = departement.NomSalle,
                TypeDept = departement.TypeDept,
                Budget = departement.Budget,
                AdminId = departement.AdminId,
                Admins = new SelectList(_context.Admins, "Id", "Nom")  // Correctement assigner la liste des admins
            };

            return View(model);
        }

        // POST: Departement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartementViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var departement = await _context.Departements.FindAsync(id);
                if (departement == null)
                    return NotFound();

                departement.NomDept = model.NomDept;
                departement.NomSalle = model.NomSalle;
                departement.TypeDept = model.TypeDept;
                departement.Budget = model.Budget;
                departement.AdminId = model.AdminId;

                _context.Departements.Update(departement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Admins = new SelectList(_context.Admins, "Id", "Nom");
            return View(model);
        }

        // GET: Departement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var departement = await _context.Departements
                .Include(d => d.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departement == null)
                return NotFound();

            return View(departement);
        }

        // POST: Departement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departement = await _context.Departements.FindAsync(id);
            _context.Departements.Remove(departement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
