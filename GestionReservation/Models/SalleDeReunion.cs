namespace GestionReservation.Models
{
    public class SalleDeReunion
    {
        public int Id { get; set; }
        public string NomSalle { get; set; }
        public int Capacite { get; set; }
        public string Localisation { get; set; }
        public string EtatSalle { get; set; }
    }
}
