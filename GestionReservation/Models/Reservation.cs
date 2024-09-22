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

        // The Employe making the reservation
        public int EmployeId { get; set; }
        public Employe Employe { get; set; }

        // The Secretaire managing the reservation
        public int SecretaireId { get; set; }
        public Secretaire Secretaire { get; set; }

        public int SalleDeReunionId { get; set; }
        public SalleDeReunion SalleDeReunion { get; set; }
    }
}
