namespace GestionReservation.Models
{
    public class Secretaire : Personne
    {
        public string NumeroBureau { get; set; }
        public string NumeroFixe { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Reservation> ManagedReservations { get; set; }
    }
}
