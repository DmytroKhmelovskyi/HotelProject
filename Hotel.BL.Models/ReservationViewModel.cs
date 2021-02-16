using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Hotel.BL;

namespace Hotel.BL.Models
{
    
    public class ReservationViewModel
    {
        public int Id { get; set; }
        [Required]
        public int? GuestId { get; set; }
        public string GuestName { get; set; }
        [Required]
        public int? RoomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        [Required]
        public int? PersonCount { get; set; }

    }
}
