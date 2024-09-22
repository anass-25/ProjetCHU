namespace GestionReservation.Models
{
    public class Employe : Personne
    {
        public string TypePost { get; set; }
        public int DepartementId { get; set; }
        public Departement Departement { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
