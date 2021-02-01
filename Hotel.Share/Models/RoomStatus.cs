using System.Collections.Generic;



namespace Hotel.Shared.Models
{
    public partial class RoomStatus
    {
        public RoomStatus()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
