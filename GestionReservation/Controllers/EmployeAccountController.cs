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
            var admin = _context.Admins.SingleOrDefault(a => a.Nom == model.Username && a.Password == model.Password);
            if (admin != null)
            {
                HttpContext.Session.SetString("Nom", admin.Nom);
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
