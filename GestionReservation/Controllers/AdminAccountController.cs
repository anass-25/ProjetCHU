using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Le constructeur injecte ApplicationDbContext
        public AdminAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /AdminAccount/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /AdminAccount/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) // Vérifie si le modèle est valide
            {
                return View(model); // Retourne la vue avec les erreurs de validation
            }

            // Utiliser EF Core pour vérifier les informations d'identification dans la base de données
            var admin = _context.Admins.SingleOrDefault(a => a.Nom == model.Username && a.Password == model.Password);

            if (admin != null)
            {
                // Définir la session
                HttpContext.Session.SetString("Nom", admin.Nom);
                HttpContext.Session.SetString("Role", "Admin");

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Message = "Nom ou mot de passe invalide.";
                return View(model);
            }
        }

    }
}

