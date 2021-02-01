using System.Collections.Generic;


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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? ReservationsCount { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
