namespace BusSeatManagement.ViewModels
{
    public class SeatChangePaymentViewModel
    {
        public int BookingId { get; set; }

        public int OldSeatId { get; set; }

        public int NewSeatId { get; set; }

        public string CurrentSeatNumber { get; set; } = string.Empty;

        public string NewSeatNumber { get; set; } = string.Empty;

        public decimal CurrentFare { get; set; }

        public decimal NewFare { get; set; }

        public decimal DifferenceAmount { get; set; }

        public string ActionType { get; set; } = string.Empty;
    }
}