using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservation.Controllers
{
    public class EmployeAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeAccountController(ApplicationDbContext context)
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
            var employe = _context.Employes.SingleOrDefault(e => e.Nom == model.Username && e.Password == model.Password);
            if (employe != null)
            {
                HttpContext.Session.SetString("Nom", employe.Nom);
                HttpContext.Session.SetString("Role", "Employe");
                return RedirectToAction("Index", "Employe");
            }
            else
            {
                ViewBag.Message = "Nom ou mot de passe invalide.";
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "EmployeAccount");
        }
    }
}
