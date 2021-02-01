using System;


namespace Hotel.Shared.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? GuestId { get; set; }
        public int? ReservationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PayTime { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
