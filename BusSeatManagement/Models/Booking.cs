using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusSeatManagement.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public string PassengerName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [ForeignKey("Bus")]
        public int BusId { get; set; }

        public Bus? Bus { get; set; }

        [ForeignKey("Seat")]
        public int CurrentSeatId { get; set; }

        public Seat? Seat { get; set; }

        public decimal OriginalFare { get; set; }

        public decimal CurrentFare { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}