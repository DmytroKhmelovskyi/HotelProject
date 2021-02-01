using System;
using System.Collections.Generic;


namespace Hotel.Shared.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int? GuestId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? PersonCount { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
