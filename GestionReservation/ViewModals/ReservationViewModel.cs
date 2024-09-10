namespace GestionReservation.ViewModals
{
    public class ReservationViewModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
