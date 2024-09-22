using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class EmployeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ReservationList()
        {
            var rooms = _context.SalleDeReunions.Select(room => new ReservationViewModel
            {
                RoomId = room.Id,
                RoomName = room.NomSalle,
                Capacity = room.Capacite,
                Location = room.Localisation,
                IsAvailable = !_context.Reservations.Any(r => r.Id == room.Id && r.DateReservation.Date == DateTime.Today.Date) // Checking availability for today
            }).ToList();

            return View(rooms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeReservation(int roomId, DateTime reservationDate)
        {
            if (ModelState.IsValid)
            {
                var existingReservation = _context.Reservations.FirstOrDefault(r => r.Id == roomId && r.DateReservation.Date == reservationDate.Date);

                if (existingReservation != null)
                {
                    TempData["ErrorMessage"] = "The room is already reserved for the selected date.";
                    return RedirectToAction("ReservationList");
                }
                var reservation = new Reservation
                {
                    Id = roomId,  
                    DateReservation = reservationDate,
                    HeureDebut = DateTime.Now, 
                    HeureFin = DateTime.Now.AddHours(2), 
                    NbParticipant = 10,  
                    Motif = "Meeting",
                };
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "The room has been reserved successfully.";
                return RedirectToAction("ReservationList");
            }
            return RedirectToAction("ReservationList");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
