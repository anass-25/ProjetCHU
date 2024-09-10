using System.ComponentModel.DataAnnotations;

namespace GestionReservation.ViewModals
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Le prénom est requis.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le CIN est requis.")]
        public string CIN { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La date de naissance est requise.")]
        [DataType(DataType.Date)]
        public DateTime DN { get; set; }

        [Required(ErrorMessage = "Le rôle est requis.")]
        public string Role { get; set; }

        // Optional Fields for Specific Roles
        public string? TypePost { get; set; }  // For Employe
        public string? NumeroBureau { get; set; }  // For Secretaire
        public string? NumeroFixe { get; set; }  // For Secretaire
        public DateTime? DateCreation { get; set; }  // For Admin
    }
}
