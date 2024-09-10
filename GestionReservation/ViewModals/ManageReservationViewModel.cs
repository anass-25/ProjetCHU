namespace GestionReservation.ViewModals
{
    public class ManageReservationViewModel
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int EmployeId { get; set; }
        public string EmployeName { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime HeureDebut { get; set; }
        public DateTime HeureFin { get; set; }
        public int NbParticipants { get; set; }
        public string Motif { get; set; }
        public bool IsAvailable { get; set; }
    }
}
