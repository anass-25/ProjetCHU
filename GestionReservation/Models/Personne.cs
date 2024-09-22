namespace GestionReservation.Models
{
    public abstract class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string CIN { get; set; }
        public string Password { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; }
        public string role { get; set; }
    }
}
