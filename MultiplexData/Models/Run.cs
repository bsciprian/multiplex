using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MultiplexData.Models
{
    public class Run
    {
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int RoomId { get; set; }
        public DateTime Date { get; set; }

        public Movie Movie { get; set; }
        public Room Room { get; set; }
        public IEnumerable<SeatRun> SeatsRun { get; set; }
    }
}
