using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionReservation.ViewModals
{
    public class DepartementViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom du département est requis.")]
        public string NomDept { get; set; }
        public string NomSalle { get; set; }
        public string TypeDept { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Le budget doit être un nombre valide.")]
        public decimal Budget { get; set; }
        [Required(ErrorMessage = "L'administrateur est requis.")]
        public int AdminId { get; set; }
        public IEnumerable<SelectListItem> Admins { get; set; }
    }
}


