using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MultiplexData.Models
{
    public class SeatRun
    {
        [Key, Column(Order=0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeatRoomId { get; set; }
        [Key, Column(Order = 1)]
        public int RunId { get; set; }
        public bool IsBooked { get; set; }

        public SeatRoom SeatRoom { get; set; }
        public Run Run { get; set; }
    }
}
