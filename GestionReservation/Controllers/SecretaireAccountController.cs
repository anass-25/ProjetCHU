using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class SecretaireAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecretaireAccountController(ApplicationDbContext context)
        {
            _context = context;
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
            var secretaire = _context.Secretaires.SingleOrDefault(s => s.Nom == model.Username && s.Password == model.Password);

            if (secretaire != null)
            {
                HttpContext.Session.SetString("Nom", secretaire.Nom);
                HttpContext.Session.SetString("Role", "Secretaire");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Nom ou mot de passe invalide.";
                return View(model);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all sessions
            return RedirectToAction("Login", "SecretaireAccount");
        }
    }
}
