using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionReservation.Controllers
{
    public class SecretaireController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecretaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ReservationList()
        {
            var reservations = _context.Reservations.Include(r => r.SalleDeReunion).ToList();
            return View(reservations);
        }

        public IActionResult CreateReservation()
        {
            ViewBag.Salles = new SelectList(_context.SalleDeReunions.ToList(), "Id", "NomSalle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReservation(Reservation model)
        {
            if (ModelState.IsValid)
            {
                _context.Reservations.Add(model);
                _context.SaveChanges();
                return RedirectToAction("ReservationList");
            }

            ViewBag.Salles = new SelectList(_context.SalleDeReunions.ToList(), "Id", "NomSalle");
            return View(model);
        }
        public IActionResult EditReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewBag.Salles = new SelectList(_context.SalleDeReunions.ToList(), "Id", "NomSalle");
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReservation(Reservation model)
        {
            if (ModelState.IsValid)
            {
                _context.Reservations.Update(model);
                _context.SaveChanges();
                return RedirectToAction("ReservationList");
            }

            ViewBag.Salles = new SelectList(_context.SalleDeReunions.ToList(), "Id", "NomSalle");
            return View(model);
        }

        public IActionResult DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Include(r => r.SalleDeReunion).FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }


        [HttpPost, ActionName("DeleteReservation")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
            return RedirectToAction("ReservationList");
        }
    }
}
