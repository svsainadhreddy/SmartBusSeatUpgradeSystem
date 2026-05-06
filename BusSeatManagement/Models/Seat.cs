using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusSeatManagement.Models
{
    public class Seat
    {
        public int SeatId { get; set; }

        [Required]
        public string SeatNumber { get; set; }

        public string SeatType { get; set; }

        public decimal Price { get; set; }

        public bool IsBooked { get; set; }

        [ForeignKey("Bus")]
        public int BusId { get; set; }

        public Bus? Bus { get; set; }
    }
}