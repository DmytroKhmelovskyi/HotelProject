using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.BL.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public int? RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomStatusName { get; set; }
        public int? RoomStatusId { get; set; }
        [Required]
        [Range(1, 100)]
        public int RoomNumber { get; set; }
        [Required]
        [Range(1, 20)]
        public int MaxPerson { get; set; }
    }
}
