namespace GestionReservation.Models
{
    public class Departement
    {
        public int Id { get; set; }
        public string NomDept { get; set; }
        public string NomSalle { get; set; }
        public string TypeDept { get; set; }
        public decimal Budget { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public ICollection<Employe> Employes { get; set; }
        public ICollection<Equipement> Equipements { get; set; }
    }
}
