using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.BL.Models
{
    public class PaymentViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int? GuestId { get; set; }
        public string GuestName { get; set; }
        public int? ReservationId { get; set; }
        [Required]
        [Range(typeof(decimal), "0,00", "1000000,00")]
        public decimal Amount { get; set; }
        public DateTime? PayTime { get; set; }
    }
}
