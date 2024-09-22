using GestionReservation.Models;

namespace GestionReservation.Models
{
    public class Admin : Personne
    {
        public DateTime DateCreation { get; set; }
        public ICollection<Departement> Departements { get; set; }
    }
}
