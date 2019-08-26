using MultiplexServices.Models.Runs;
using System;
using System.Collections.Generic;

namespace MultiplexServices.Models.Rooms
{ 
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Seats { get; set; }
    }
}
