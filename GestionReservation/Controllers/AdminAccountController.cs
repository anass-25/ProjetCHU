using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminAccountController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }
            var admin = _context.Admins.SingleOrDefault(a => a.Nom == model.Username && a.Password == model.Password);

            if (admin != null)
            {
                HttpContext.Session.SetString("Nom", admin.Nom);
                HttpContext.Session.SetString("Role", "Admin");

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Nom ou mot de passe invalide.");
                return View(model);
            }
        }
    }
}

