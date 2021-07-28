using System;
using System.Collections.Generic;
using System.Text;

namespace TripAverage
{
    public class Trip
    {
        public Driver Driver { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Distance { get; set; }
    }
}
