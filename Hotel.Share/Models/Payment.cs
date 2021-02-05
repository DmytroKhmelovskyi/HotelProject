using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Shared.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? GuestId { get; set; }
        public int? ReservationId { get; set; }

        [Range(typeof(decimal), "0,00", "1000000,00")]
        public decimal Amount { get; set; }
        public DateTime? PayTime { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
