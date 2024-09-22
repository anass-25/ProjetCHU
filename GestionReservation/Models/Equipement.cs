namespace GestionReservation.Models
{
    public class Equipement
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Etat { get; set; }
        public int DepartementId { get; set; }
        public Departement Departement { get; set; }
    }
}
