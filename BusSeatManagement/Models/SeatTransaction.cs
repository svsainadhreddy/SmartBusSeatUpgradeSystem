using System.ComponentModel.DataAnnotations.Schema;

namespace BusSeatManagement.Models
{
    public class SeatTransaction
    {
        public int SeatTransactionId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }

        public Booking? Booking { get; set; }

        public int OldSeatId { get; set; }
        public int NewSeatId { get; set; }

        public decimal OldFare { get; set; }

        public decimal NewFare { get; set; }

        public decimal DifferenceAmount { get; set; }

        public string ActionType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}