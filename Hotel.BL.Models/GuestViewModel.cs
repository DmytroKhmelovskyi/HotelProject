using System.ComponentModel.DataAnnotations;

namespace Hotel.BL.Models
{
    public class GuestViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
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

    }
}
