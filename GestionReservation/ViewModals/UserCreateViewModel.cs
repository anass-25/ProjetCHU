namespace GestionReservation.ViewModals
{
    public class UserCreateViewModel
    {
        public string UserName { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string CIN { get; set; }
        public string Password { get; set; } 
        public DateTime? DN { get; set; }    
        public DateTime? DateCreation { get; set; } 
        public string TypePost { get; set; }
        public int? DepartementId { get; set; }      
        public string NumeroBureau { get; set; }
        public string NumeroFixe { get; set; }
        public string Role { get; set; }
    }
}
