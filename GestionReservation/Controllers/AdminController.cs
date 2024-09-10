using GestionReservation.Models;
using GestionReservation.ViewModals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GestionReservation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserList()
        {
            var users = _context.Employes
                .Select(e => new UserViewModel
                {
                    Id = e.Id,
                    Nom = e.Nom,
                    Prenom = e.Prenom,
                    Email = e.Email,
                    Role = "Employe"
                })
                .Union(_context.Secretaires
                    .Select(s => new UserViewModel
                    {
                        Id = s.Id,
                        Nom = s.Nom,
                        Prenom = s.Prenom,
                        Email = s.Email,
                        Role = "Secretaire"
                    }))
                .Union(_context.Admins
                    .Select(a => new UserViewModel
                    {
                        Id = a.Id,
                        Nom = a.Nom,
                        Prenom = a.Prenom,
                        Email = a.Email,
                        Role = "Admin"
                    }))
                .ToList();

            return View(users);
        }

        // GET: AdminAccount/EditUser/5
        public IActionResult EditUser(int id, string role)
        {
            if (role == "Employe")
            {
                var employe = _context.Employes.Find(id);
                if (employe == null) return NotFound();

                var model = new UserCreateViewModel
                {
                    UserName = employe.Nom,
                    Prenom = employe.Prenom,
                    Email = employe.Email,
                    CIN = employe.CIN,
                    Password = employe.Password,
                    DN = employe.DateNaissance,
                    Role = "Employe",
                    TypePost = employe.TypePost
                };

                return View(model);
            }
            else if (role == "Secretaire")
            {
                var secretaire = _context.Secretaires.Find(id);
                if (secretaire == null) return NotFound();

                var model = new UserCreateViewModel
                {
                    UserName = secretaire.Nom,
                    Prenom = secretaire.Prenom,
                    Email = secretaire.Email,
                    CIN = secretaire.CIN,
                    Password = secretaire.Password,
                    DN = secretaire.DateNaissance,
                    Role = "Secretaire",
                    NumeroBureau = secretaire.NumeroBureau,
                    NumeroFixe = secretaire.NumeroFixe
                };

                return View(model);
            }
            else if (role == "Admin")
            {
                var admin = _context.Admins.Find(id);
                if (admin == null) return NotFound();

                var model = new UserCreateViewModel
                {
                    UserName = admin.Nom,
                    Prenom = admin.Prenom,
                    Email = admin.Email,
                    CIN = admin.CIN,
                    Password = admin.Password,
                    DN = admin.DateNaissance,
                    Role = "Admin",
                    DateCreation = admin.DateCreation
                };

                return View(model);
            }

            return NotFound();
        }

        // POST: AdminAccount/DeleteUser/5
        [HttpPost]
        public IActionResult DeleteUser(int id, string role)
        {
            if (role == "Employe")
            {
                var employe = _context.Employes.Find(id);
                if (employe != null)
                {
                    _context.Employes.Remove(employe);
                }
            }
            else if (role == "Secretaire")
            {
                var secretaire = _context.Secretaires.Find(id);
                if (secretaire != null)
                {
                    _context.Secretaires.Remove(secretaire);
                }
            }
            else if (role == "Admin")
            {
                var admin = _context.Admins.Find(id);
                if (admin != null)
                {
                    _context.Admins.Remove(admin);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("UserList");
        }
        public IActionResult CreateUser()
        {
            return View();
        }
       

        // POST: AdminAccount/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If model is not valid, return the same view with validation messages
                return View(model);
            }

            try
            {
                if (model.Role == "Employe")
                {
                    var employe = new Employe
                    {
                        Nom = model.UserName,
                        Prenom = model.Prenom,
                        Email = model.Email,
                        CIN = model.CIN,
                        Password = model.Password, // Ensure this is hashed in a real app
                        DateNaissance = model.DN,
                        TypePost = model.TypePost
                    };
                    _context.Employes.Add(employe);
                }
                else if (model.Role == "Secretaire")
                {
                    var secretaire = new Secretaire
                    {
                        Nom = model.UserName,
                        Prenom = model.Prenom,
                        Email = model.Email,
                        CIN = model.CIN,
                        Password = model.Password, // Ensure this is hashed in a real app
                        DateNaissance = model.DN,
                        NumeroBureau = model.NumeroBureau,
                        NumeroFixe = model.NumeroFixe
                    };
                    _context.Secretaires.Add(secretaire);
                }
                else if (model.Role == "Admin")
                {
                    var admin = new Admin
                    {
                        Nom = model.UserName,
                        Prenom = model.Prenom,
                        Email = model.Email,
                        CIN = model.CIN,
                        Password = model.Password, // Ensure this is hashed in a real app
                        DateNaissance = model.DN,
                        DateCreation = model.DateCreation ?? DateTime.Now
                    };
                    _context.Admins.Add(admin);
                }

                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index", "Admin"); // Redirect after successful creation
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                ViewBag.ErrorMessage = "An error occurred while saving the user.";
                return View(model);
            }
        }
    }
}
