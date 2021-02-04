using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Shared.Models
{
    public partial class Guest
    {
        public Guest()
        {
            Payments = new HashSet<Payment>();
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength =2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [StringLength(30, MinimumLength = 2)]
        public string City { get; set; }
        [StringLength(20, MinimumLength = 2)]
        public string Country { get; set; }
        public int? ReservationsCount { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
