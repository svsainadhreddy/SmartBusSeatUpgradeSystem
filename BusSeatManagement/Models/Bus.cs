using System.ComponentModel.DataAnnotations;

namespace BusSeatManagement.Models
{
    public class Bus
    {
        public int BusId { get; set; }

        [Required]
        public string BusName { get; set; }

        public int TotalSeats { get; set; }

        public ICollection<Seat>? Seats { get; set; }
    }
}