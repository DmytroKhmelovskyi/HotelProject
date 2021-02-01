using System.Collections.Generic;


namespace Hotel.Shared.Models
{
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int? RoomTypeId { get; set; }
        public int? RoomStatusId { get; set; }
        public int RoomNumber { get; set; }
        public int MaxPerson { get; set; }

        public virtual RoomStatus RoomStatus { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
