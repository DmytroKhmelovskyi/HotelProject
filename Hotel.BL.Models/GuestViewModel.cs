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
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string City { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Country { get; set; }
        public int? ReservationsCount { get; set; }

    }
}
