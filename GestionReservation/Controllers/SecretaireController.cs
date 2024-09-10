using GestionReservation.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class SecretaireController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecretaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        // GET: List of reservations for employees
        public IActionResult ManageReservations()
        {
            // Get the list of reservations
            var reservations = _context.Reservations.Include(r => r.Room).Include(r => r.Employe)
                .Select(reservation => new ManageReservationViewModel
                {
                    ReservationId = reservation.Id,
                    RoomId = reservation.RoomId,
                    RoomName = reservation.Room.NomSalle,
                    EmployeId = reservation.EmployeId,
                    EmployeName = $"{reservation.Employe.Nom} {reservation.Employe.Prenom}",
                    ReservationDate = reservation.Date,
                    HeureDebut = reservation.HeureDebut,
                    HeureFin = reservation.HeureFin,
                    NbParticipants = reservation.NbParticipants,
                    Motif = reservation.Motif
                }).ToList();

            return View(reservations);
        }

        // GET: Create a new reservation
        public IActionResult CreateReservation()
        {
            ViewBag.Rooms = _context.SalleDeReunions.ToList();
            ViewBag.Employes = _context.Employes.ToList();
            return View();
        }

        // POST: Create a new reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReservation(ManageReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    RoomId = model.RoomId,
                    EmployeId = model.EmployeId,
                    Date = model.ReservationDate,
                    HeureDebut = model.HeureDebut,
                    HeureFin = model.HeureFin,
                    NbParticipants = model.NbParticipants,
                    Motif = model.Motif
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Reservation created successfully.";
                return RedirectToAction("ManageReservations");
            }

            ViewBag.Rooms = _context.SalleDeReunions.ToList();
            ViewBag.Employes = _context.Employes.ToList();
            return View(model);
        }

        // GET: Edit an existing reservation
        public IActionResult EditReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var model = new ManageReservationViewModel
            {
                ReservationId = reservation.Id,
                RoomId = reservation.RoomId,
                EmployeId = reservation.EmployeId,
                ReservationDate = reservation.Date,
                HeureDebut = reservation.HeureDebut,
                HeureFin = reservation.HeureFin,
                NbParticipants = reservation.NbParticipants,
                Motif = reservation.Motif
            };

            ViewBag.Rooms = _context.SalleDeReunions.ToList();
            ViewBag.Employes = _context.Employes.ToList();

            return View(model);
        }

        // POST: Edit an existing reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReservation(ManageReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reservation = _context.Reservations.Find(model.ReservationId);

                if (reservation == null)
                {
                    return NotFound();
                }

                reservation.RoomId = model.RoomId;
                reservation.EmployeId = model.EmployeId;
                reservation.Date = model.ReservationDate;
                reservation.HeureDebut = model.HeureDebut;
                reservation.HeureFin = model.HeureFin;
                reservation.NbParticipants = model.NbParticipants;
                reservation.Motif = model.Motif;

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Reservation updated successfully.";
                return RedirectToAction("ManageReservations");
            }

            ViewBag.Rooms = _context.SalleDeReunions.ToList();
            ViewBag.Employes = _context.Employes.ToList();
            return View(model);
        }

        // POST: Delete a reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);

            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Reservation deleted successfully.";
            return RedirectToAction("ManageReservations");
        }
       
    }
}
