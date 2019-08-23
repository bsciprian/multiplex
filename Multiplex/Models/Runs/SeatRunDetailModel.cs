using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiplex.Models.Runs
{
    namespace Multiplex.Models.Runs
    {
        public class SeatRunDetailModel
        {
            public string SeatName { get; set; }
            public bool IsBooked { get; set; }

            public SeatRunDetailModel(string seatName, bool isBooked)
            {
                SeatName = seatName;
                IsBooked = isBooked;
            }
        }
    }
}
