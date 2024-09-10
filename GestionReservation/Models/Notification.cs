namespace GestionReservation.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime DateEnvoie { get; set; }
        public string Etat { get; set; }
    }
}
