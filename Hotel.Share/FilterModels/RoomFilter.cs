using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Shared.FilterModels
{
   public class RoomFilter
    {
        public int RoomNumber { get; set; }
        public int MaxPerson { get; set; }
        public string SortOrder { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
