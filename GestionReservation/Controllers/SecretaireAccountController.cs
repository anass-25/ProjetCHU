using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class SecretaireAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Le constructeur injecte ApplicationDbContext
        public SecretaireAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /SecretaireAccount/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /SecretaireAccount/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string nom, string password)
        {
            // Utiliser EF Core pour vérifier les informations d'identification dans la base de données
            var secretaire = _context.Secretaires.SingleOrDefault(s => s.Nom == nom && s.Password == password);

            if (secretaire != null)
            {
                // Définir la session
                HttpContext.Session.SetString("Nom", secretaire.Nom);
                HttpContext.Session.SetString("Role", "Secretaire");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Nom ou mot de passe invalide.";
                return View();
            }
        }

        // GET: /SecretaireAccount/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Effacer toutes les sessions
            return RedirectToAction("Login", "SecretaireAccount");
        }
    }
}
