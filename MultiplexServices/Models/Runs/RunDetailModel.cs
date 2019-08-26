using MultiplexServices.Models.Runs;
using System;
using System.Collections.Generic;

namespace MultiplexServices.Models.Runs
{
    public class RunDetailModel
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string MoviePoster { get; set; }
        public TimeSpan MovieDuration{ get; set; }
        public string MovieType { get; set; }
        public string MovieDescription { get; set; }
        public DateTime DateTime { get; set; }
        public List<SeatRunDetailModel> Seats { get; set; }
        public List<int> Rows { get; set; }
    }
}
