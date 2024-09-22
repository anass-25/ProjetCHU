namespace GestionReservation.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateEnvoie { get; set; }
        public string Etat { get; set; }

        public int? SecretaireId { get; set; }
        public Secretaire Secretaire { get; set; }

        public int? EmployeId { get; set; }
        public Employe Employe { get; set; }

        public bool IsFromSecretaire { get; set; }
    }
}
