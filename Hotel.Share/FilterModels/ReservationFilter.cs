﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Shared.FilterModels
{
    public class ReservationFilter
    {
        public string SortOrder { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
