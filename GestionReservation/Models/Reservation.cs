namespace GestionReservation.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime HeureDebut { get; set; }
        public DateTime HeureFin { get; set; }
        public string Motif { get; set; }
        public int NbParticipant { get; set; }
    }
}
