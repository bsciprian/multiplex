using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultiplexData.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string RoomName { get; set; }
        public string SeatsNumber { get; set; }
        public IEnumerable<SeatRoom> SeatsRoom {get; set;}
    }
}
