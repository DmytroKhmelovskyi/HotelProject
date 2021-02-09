using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.VIewModel
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public int? GuestId { get; set; }
        public string GuestName { get; set; }
        public int? RoomId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? PersonCount { get; set; }

    }
}
