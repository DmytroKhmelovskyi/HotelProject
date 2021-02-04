using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Shared.Models
{
    public partial class RoomStatus
    {
        public RoomStatus()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Status { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
