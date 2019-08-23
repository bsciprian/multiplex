using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplexData.Models
{
    public class SeatRoom
    {
        public int Id { get; set; }
        public string SeatName { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
