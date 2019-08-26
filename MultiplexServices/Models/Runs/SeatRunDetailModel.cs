using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MultiplexServices.Models.Runs
{
    public class SeatRunDetailModel
    {
        public int RunId { get; set; }
        public int SeatRoomID { get; set; }
        public string SeatName { get; set; }
        public bool IsBooked { get; set; }

        public SeatRunDetailModel(int runId, int seatRoomID, string seatName, bool isBooked)
        {
            RunId = runId;
            SeatRoomID = seatRoomID;
            SeatName = seatName;
            IsBooked = isBooked;
        }
    }
}

