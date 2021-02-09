using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.VIewModel
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public int? GuestId { get; set; }
        public string GuestName { get; set; }
        public int? ReservationId { get; set; }

        [Range(typeof(decimal), "0,00", "1000000,00")]
        public decimal Amount { get; set; }
        public DateTime? PayTime { get; set; }
    }
}
