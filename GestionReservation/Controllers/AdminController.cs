using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Index (Main Dashboard View)
        public IActionResult Index()
        {
            return View();
        }

        #region User List

        // GET: User List (Displays all users)
        public async Task<IActionResult> UserList()
        {
            var employes = await _context.Employes
         .Select(e => new UserViewModel
         {
             Id = e.Id,
             Nom = e.Nom,
             Prenom = e.Prenom,
             Email = e.Email,
             Role = "Employe"
         })
         .ToListAsync();

            var secretaires = await _context.Secretaires
                .Select(s => new UserViewModel
                {
                    Id = s.Id,
                    Nom = s.Nom,
                    Prenom = s.Prenom,
                    Email = s.Email,
                    Role = "Secretaire"
                })
                .ToListAsync();

            var admins = await _context.Admins
                .Select(a => new UserViewModel
                {
                    Id = a.Id,
                    Nom = a.Nom,
                    Prenom = a.Prenom,
                    Email = a.Email,
                    Role = "Admin"
                })
                .ToListAsync();

            // Combine the lists into one list of UserViewModel
            var users = employes
                .Union(secretaires)
                .Union(admins)
                .ToList();

            return View(users);
        }

        #endregion

        #region Create Admin

        // GET: Create Admin (Form View)
        public IActionResult CreateAdmin()
        {
            return View(new UserCreateViewModel { Role = "Admin" });
        }

        // POST: Create Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Les données du modèle ne sont pas valides";
                return View(model);
            }

            try
            {
                var admin = new Admin
                {
                    Nom = model.UserName,
                    Prenom = model.Prenom,
                    Email = model.Email,
                    CIN = model.CIN,
                    Password = model.Password, // Make sure to hash this in production
                    DateNaissance = model.DN.Value,
                    DateCreation = model.DateCreation ?? DateTime.Now
                };

                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();
                TempData["successMessage"] = "Admin créé avec succès";
                return RedirectToAction(nameof(UserList));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Erreur lors de la création : {ex.Message}";
                return View(model);
            }
        }

        #endregion

        #region Create Employe

        // GET: Create Employe (Form View)
        public async Task<IActionResult> CreateEmploye()
        {
            ViewBag.Departements = new SelectList(await _context.Departements.ToListAsync(), "Id", "NomDept");
            return View(new UserCreateViewModel { Role = "Employe" });
        }

        // POST: Create Employe (Handles form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmploye(UserCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employe = new Employe
                    {
                        Nom = model.UserName,
                        Prenom = model.Prenom,
                        Email = model.Email,
                        CIN = model.CIN,
                        Password = model.Password,
                        DateNaissance = model.DN.Value,
                        TypePost = model.TypePost,
                        DepartementId = model.DepartementId.Value
                    };

                    _context.Employes.Add(employe);
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Employé créé avec succès";
                    return RedirectToAction(nameof(UserList));
                }

                ViewBag.Departements = new SelectList(await _context.Departements.ToListAsync(), "Id", "NomDept");
                TempData["errorMessage"] = "Les données du modèle ne sont pas valides";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Une erreur s'est produite: {ex.Message}";
                ViewBag.Departements = new SelectList(await _context.Departements.ToListAsync(), "Id", "NomDept");
                return View(model);
            }
        }

        #endregion

        #region Create Secretaire

        // GET: Create Secretaire (Form View)
        public IActionResult CreateSecretaire()
        {
            return View(new UserCreateViewModel { Role = "Secretaire" });
        }

        // POST: Create Secretaire (Handles form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSecretaire(UserCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var secretaire = new Secretaire
                    {
                        Nom = model.UserName,
                        Prenom = model.Prenom,
                        Email = model.Email,
                        CIN = model.CIN,
                        Password = model.Password,
                        DateNaissance = model.DN.Value,
                        NumeroBureau = model.NumeroBureau,
                        NumeroFixe = model.NumeroFixe
                    };

                    _context.Secretaires.Add(secretaire);
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Secrétaire créé avec succès";
                    return RedirectToAction(nameof(UserList));
                }

                TempData["errorMessage"] = "Les données du modèle ne sont pas valides";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Une erreur s'est produite: {ex.Message}";
                return View(model);
            }
        }

        #endregion

        #region Delete User Methods

        // POST: Delete User (Handles deletion)
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id, string role)
        {
            try
            {
                switch (role)
                {
                    case "Employe":
                        var employe = await _context.Employes.FindAsync(id);
                        if (employe != null) _context.Employes.Remove(employe);
                        break;
                    case "Secretaire":
                        var secretaire = await _context.Secretaires.FindAsync(id);
                        if (secretaire != null) _context.Secretaires.Remove(secretaire);
                        break;
                    case "Admin":
                        var admin = await _context.Admins.FindAsync(id);
                        if (admin != null) _context.Admins.Remove(admin);
                        break;
                    default:
                        TempData["errorMessage"] = "Rôle invalide";
                        return NotFound();
                }

                await _context.SaveChangesAsync();
                TempData["successMessage"] = "Utilisateur supprimé avec succès";
                return RedirectToAction(nameof(UserList));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"Une erreur s'est produite lors de la suppression: {ex.Message}";
                return RedirectToAction(nameof(UserList));
            }
        }


        public async Task<IActionResult> AdminList()
        {
            var admins = await _context.Admins
                .Select(a => new UserViewModel
                {
                    Id = a.Id,
                    Nom = a.Nom,
                    Prenom = a.Prenom,
                    Email = a.Email,
                    Role = "Admin"
                })
                .ToListAsync();

            return View(admins);
        }

        #endregion
    }
}
